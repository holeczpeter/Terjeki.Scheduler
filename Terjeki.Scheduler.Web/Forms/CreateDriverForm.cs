namespace Terjeki.Scheduler.Web.Components.Drivers
{
    public class CreateDriverForm : INotifyPropertyChanged
    {
        private bool _isExistingUser;
        public bool IsExistingUser
        {
            get => _isExistingUser;
            set
            {
                if (_isExistingUser != value)
                {
                    _isExistingUser = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExistingUser)));

                   
                    if (_isExistingUser)
                        DriverName = string.Empty;
                    else
                        DriverUserId = null;

              
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DriverUserId)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DriverName)));
                }
            }
        }
        private Guid? _driverUserId;
        
        public Guid? DriverUserId
        {
            get => _driverUserId;
            set
            {
                if (_driverUserId != value)
                {
                    _driverUserId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DriverUserId)));
                }
            }
        }
       
        private string _driverName = string.Empty;
        
        public string DriverName
        {
            get => _driverName;
            set
            {
                if (_driverName != value)
                {
                    _driverName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DriverName)));
                }
            }
        }

        private BusModel _bus;
        public BusModel Bus
        {
            get => _bus;
            set
            {
                if (_bus != value)
                {
                    _bus = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bus)));
                }
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
