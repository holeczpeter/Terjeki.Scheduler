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

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            AppDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _dbContext = dbContext;
        }

        // ### 4.1. Regisztráció ###
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var allowed = await _dbContext.AllowedEmails
            .FirstOrDefaultAsync(a => a.Email == dto.Email);
            if (allowed == null)
                return Forbid("Ehhez az e-mail címhez nincs regisztrációs engedély.");

            if (allowed.IsUsed)
                return BadRequest("Ezzel az e-maillel már regisztráltak.");

            // 2) Felhasználó létrehozása
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            // 3) kártyázzuk a role-t a AllowedEmails.RoleName alapján
            await _userManager.AddToRoleAsync(user, allowed.RoleName);
            // 3) Jelöljük, hogy használták az engedélyt
            allowed.IsUsed = true;
            await _dbContext.SaveChangesAsync();

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = $"{Request.Scheme}://{Request.Host}/api/account/confirm-email?"
                        + $"userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
            return Ok(new { message = "Sikeres regisztráció. Ellenőrizd az e-mailjeidet." });
            
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return BadRequest("Invalid user.");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded) return BadRequest("Email confirmation failed.");
            return Ok("Email confirmed.");
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

        // ### 4.6. Jelszó visszaállítás ###
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Ok(); // ne áruld el, hogy nincs ilyen email

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = $"{Request.Scheme}://{Request.Host}/reset-password?email={dto.Email}&token={HttpUtility.UrlEncode(token)}";
            // TODO: küldd el emailben a url-t
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return BadRequest("Invalid user.");

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded) return BadRequest("Password reset failed.");

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
