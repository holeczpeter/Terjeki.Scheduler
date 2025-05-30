namespace Terjeki.Scheduler.Web.Pages
{

    public class LoginForm : INotifyPropertyChanged
    {
       
        private string _email;

        [Required(ErrorMessage = "Az e-mail cím megadása kötelező.")]
        [EmailAddress]
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }
        private string _password;
        [Required(ErrorMessage = "A jelszó megadása kötelező."), MinLength(8, ErrorMessage = "A jelszó legalább 8 karakter hosszú legyen")]
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
