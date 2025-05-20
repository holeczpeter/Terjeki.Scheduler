using Terjeki.Scheduler.Core;

namespace Terjeki.Scheduler.Web.Components.Users
{
    public class CreateUserForm : INotifyPropertyChanged
    {
        private string _name;

        [Required(ErrorMessage = "A név megadása kötelező.")]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }
        private string _email;

        [Required(ErrorMessage = "Az e-mail cím megadása kötelező.")]
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
        private RoleModel _role;

        public RoleModel Role
        {
            get => _role;
            set
            {
                if (_role != value)
                {
                    _role = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Role)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
