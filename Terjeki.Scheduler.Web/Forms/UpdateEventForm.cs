using Terjeki.Scheduler.Core;

namespace Terjeki.Scheduler.Web.Components.Event
{
    public class UpdateEventForm : INotifyPropertyChanged
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
        private CapacityModel _capacity;

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
        private List<DriverModel> _drivers;

        public List<DriverModel> Drivers
        {
            get => _drivers;
            set
            {
                if (_drivers != value)
                {
                    _drivers = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drivers)));
                }
            }
        }
        private DateTime _start;

        public DateTime Start
        {
            get => _start;
            set
            {
                if (_start != value)
                {
                    _start = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Start)));
                }
            }
        }
        private DateTime _end;

        public DateTime End
        {
            get => _end;
            set
            {
                if (_end != value)
                {
                    _end = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(End)));
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
