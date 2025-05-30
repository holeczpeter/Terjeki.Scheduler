namespace Terjeki.Scheduler.Web.Forms
{
    public class RegisterForm
    {
        [Required(ErrorMessage = "A név megadása kötelező.")] 
        public string FullName { get; set; } = "";
        [Required(ErrorMessage = "Az e-mail cím megadása kötelező."), EmailAddress(ErrorMessage = "Nem megfelelő e-mail cím formátum")] 
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "A jelszó megadása kötelező."), MinLength(8, ErrorMessage = "A jelszó legalább 8 karakter hosszú legyen")]
        public string Password { get; set; } = "";
        [Required(ErrorMessage = "A jelszó megadása kötelező.")] 
        public string ConfirmPassword { get; set; } = "";
    }
}
