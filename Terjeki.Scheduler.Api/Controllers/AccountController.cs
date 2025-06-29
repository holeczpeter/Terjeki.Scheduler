namespace Terjeki.Scheduler.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Web; // for UrlEncoder
    using Terjeki.Scheduler.Core.Entities;
    using Terjeki.Scheduler.Core.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;
        private readonly IEmailService _emailService;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            AppDbContext dbContext,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _dbContext = dbContext;
            _emailService = emailService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var allowed = await _dbContext.AllowedEmails.FirstOrDefaultAsync(a => a.Email == dto.Email);
            if (allowed == null) return Forbid("Ehhez az e-mail címhez nincs regisztrációs engedély.");
            if (allowed.IsUsed) return BadRequest("Ezzel az e-maillel már regisztráltak.");

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, allowed.RoleName);

            allowed.IsUsed = true;
            await _dbContext.SaveChangesAsync();

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var confirmUrl = $"{Request.Scheme}://{Request.Host}/api/account/confirm-email?" +
                             $"userId={user.Id}&token={encodedToken}";
            var htmlBody = $@"
                        <!DOCTYPE html>
                        <html lang=""hu"">
                        <head>
                          <meta charset=""UTF-8"" />
                          <style>
                            body {{ font-family: Arial, sans-serif; color: #333; }}
                            .btn {{ display: inline-block; padding: 10px 20px; background: #005CAF; color: #fff; text-decoration: none; border-radius: 5px; }}
                          </style>
                        </head>
                        <body>
                          <h2>Kedves {dto.FullName}!</h2>
                          <p>Köszönjük, hogy regisztráltál. Kérlek, erősítsd meg az e-mail címed az alábbi gombra kattintva:</p>
                          <p><a class=""btn"" href=""{confirmUrl}"">E-mail cím megerősítése</a></p>
                          <p>Ha a gomb nem működik, másold be ezt az URL-t a böngésződbe:</p>
                          <p><code>{confirmUrl}</code></p>
                          <hr />
                          <p>A Te Csapatod</p>
                        </body>
                        </html>";

            await _emailService.SendEmailAsync(
                to: dto.Email,
                subject: "Fiókod megerősítése",
                htmlBody: htmlBody);


            return Ok(new { message = "Sikeres regisztráció. Ellenőrizd az e-mailjeidet." });

        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            if (userId == Guid.Empty || string.IsNullOrWhiteSpace(token))
                return Redirect($"{_config["ClientAppUrl"]}/email-confirmation-failed");

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return Redirect($"{_config["ClientAppUrl"]}/email-confirmation-failed");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {

                return Redirect($"{_config["ClientAppUrl"]}/email-confirmed");
            }
            else
            {

                return Redirect($"{_config["ClientAppUrl"]}/email-confirmation-failed");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid credentials.");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized("Email not confirmed.");

            // 2FA kezelése…
            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Forbid("RequiresTwoFactor");
            }

            var jwt = await GenerateJwtTokenAsync(user);
            return Ok(new { token = jwt });
        }

        // ### 4.3. 2FA kód generálása (Authenticator App) ###
        [Authorize]
        [HttpGet("2fa/setup")]
        public async Task<IActionResult> Setup2Fa()
        {
            var user = await _userManager.GetUserAsync(User)!;

            // 1) Először lekérjük a kulcsot
            var key = await _userManager.GetAuthenticatorKeyAsync(user);

            // 2) Ha még nincs, reseteljük és kérjük le újra
            if (string.IsNullOrEmpty(key))
            {
                var resetResult = await _userManager.ResetAuthenticatorKeyAsync(user);
                if (!resetResult.Succeeded)
                    return BadRequest("Unable to reset 2FA key.");

                key = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            // 3) QR URI összeállítása
            var issuer = _config["Jwt:Issuer"]!;
            var encodedEmail = Uri.EscapeDataString(user.Email!);
            var qrUri = $"otpauth://totp/{issuer}:{encodedEmail}?secret={key}&issuer={issuer}&digits=6";

            return Ok(new { qrUri, secret = key });
        }
        // ### 4.4. 2FA engedélyezése ###
        [Authorize]
        [HttpPost("2fa/enable")]
        public async Task<IActionResult> Enable2Fa(Enable2FaDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(
                user,
                _userManager.Options.Tokens.AuthenticatorTokenProvider,
                dto.Code);

            if (!isValid) return BadRequest("Invalid 2FA code.");

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            return Ok("2FA enabled.");
        }

        // ### 4.5. 2FA belépés ellenőrzés ###
        [HttpPost("login/2fa")]
        public async Task<IActionResult> LoginWith2Fa(LoginWith2FaDto dto)
        {
            // A pozícionális hívás: (code, isPersistent: rememberMe, rememberClient)
            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
                dto.Code,
                isPersistent: false,        // ne maradjon a böngészőben a session
                rememberClient: dto.RememberClient
            );

            if (!result.Succeeded)
                return Unauthorized("Invalid 2FA code.");

            // Ha sikerült, újra generáld a JWT-t
            var user = await _signInManager.UserManager.GetUserAsync(User)!;
            var jwt = await GenerateJwtTokenAsync(user);
            return Ok(new { token = jwt });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel dto)
        {
            // 1) Keresd meg a felhasználót e-mail alapján
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Ok();
           
            // 2) Generáld le a reset tokent, és kódold URL-hez
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);

            // 3) Állítsd össze a reset linket (frontend URL-re mutat)
    
            var resetLink = $"{_config["ClientAppUrl"]}/reset-password/" +
                 $"{HttpUtility.UrlEncode(dto.Email)}/" +
                 $"{encodedToken}";
            // 4) Készítsd el az e-mail modellünket a sablonhoz
            var emailModel = new ForgotPasswordEmailModel
            {
                UserName = user.FullName,
                ResetLink = resetLink,
                ExpirationMinutes = 60,
                SupportEmail = "support@terjeki-scheduler.hu",
                CompanyName = "Terjéki Naptár csapata"
            };

            // 5) Rendereld le a HTML és plain-text törzset (akár RazorLight-dal, itt inline példával)
            var htmlBody = $@"
                        <!DOCTYPE html>
                        <html lang=""hu"">
                        <head>
                          <meta charset=""utf-8"" />
                          <style>
                            body {{ font-family: Arial, sans-serif; color: #333; }}
                            .container {{ max-width: 600px; margin: auto; padding: 20px; }}
                            .button {{
                              display: inline-block;
                              padding: 12px 24px;
                              background-color: #0078D4;
                              color: white;
                              text-decoration: none;
                              border-radius: 4px;
                            }}
                            .footer {{ font-size: 0.9em; color: #777; margin-top: 30px; }}
                          </style>
                        </head>
                        <body>
                          <div class=""container"">
                            <h2>Jelszó visszaállítás</h2>
                            <p>Szervusz {emailModel.UserName},</p>
                            <p>Úgy tűnik, kérted a jelszavad visszaállítását a Terjéki Naptárban. Kattints az alábbi gombra az új jelszó beállításához:</p>
                            <p><a class=""button"" href=""{emailModel.ResetLink}"" target=""_blank"">Jelszó visszaállítása</a></p>
                            <p>A gomb <strong>{emailModel.ExpirationMinutes}</strong> percen belül érvényes.</p>
                            <p>Ha nem Te kérted, hagyd figyelmen kívül ezt az üzenetet.</p>
                            <div class=""footer"">
                              <p>Üdvözlettel,<br />{emailModel.CompanyName}</p>
                              <p>Segítség: <a href=""mailto:{emailModel.SupportEmail}"">{emailModel.SupportEmail}</a></p>
                            </div>
                          </div>
                        </body>
                        </html>";



            // 6) E-mail küldése
            await _emailService.SendEmailAsync(
                to: dto.Email,
                subject: "Jelszó visszaállítási kérelem",
                htmlBody: htmlBody
                
            );

            return Ok();
        }

        // ### 4.7. Jelszó visszaállítás lefuttatása ###
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel dto)
        {
            
            // 1) Felhasználó lekérése
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return BadRequest("Érvénytelen felhasználó.");

            // 2) Jelszó visszaállítása a token és az új jelszó alapján
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded)return BadRequest("A jelszó visszaállítása nem sikerült.");

            return Ok();
        }


        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,   user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(ClaimTypes.Name,               user.FullName),
                new Claim(ClaimTypes.NameIdentifier,     user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expires = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
