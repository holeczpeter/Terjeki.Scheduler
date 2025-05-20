using Terjeki.Scheduler.Core;

namespace Terjeki.Scheduler.Web.Components.Buses
{
    public class UpdateBusForm : INotifyPropertyChanged
    {
        private Guid _id;

        [Required]
        public Guid Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_id)));
                }
            }
        }
        private string _brand;

        [Required(ErrorMessage = "A név megadása kötelező.")]
        public string Brand
        {
            get => _brand;
            set
            {
                if (_brand != value)
                {
                    _brand = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Brand)));
                }
            }
        }
        private string _licensePlateNumber;

        [Required(ErrorMessage = "A rendszám megadása kötelező.")]
        public string LicensePlateNumber
        {
            get => _licensePlateNumber;
            set
            {
                if (_licensePlateNumber != value)
                {
                    _licensePlateNumber = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LicensePlateNumber)));
                }
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }

        private CapacityModel _capacity;

        [Required(ErrorMessage = "A férőhelyek számának megadása kötelező.")]
        public CapacityModel Capacity
        {
            get => _capacity;
            set
            {
                if (_capacity != value)
                {
                    _capacity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Capacity)));
                }
            }
        }
        private DriverModel _driver;

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
        private int _currentMileage;

        public int CurrentMileage
        {
            get => _currentMileage;
            set
            {
                if (_currentMileage != value)
                {
                    _currentMileage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMileage)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
