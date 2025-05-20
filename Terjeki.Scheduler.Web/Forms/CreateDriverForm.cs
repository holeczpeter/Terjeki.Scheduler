using Terjeki.Scheduler.Core;

namespace Terjeki.Scheduler.Web.Components.Drivers
{
    public class CreateDriverForm : INotifyPropertyChanged
    {
        private DriverModel _driver;

        [Required(ErrorMessage = "A személy megadása kötelező.")]
        public DriverModel Driver
        {
            get => _driver;
            set
            {
                if (_driver != value)
                {
                    _driver = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Driver)));
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
