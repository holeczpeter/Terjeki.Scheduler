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
        private BusItemModel _bus;

        public BusItemModel Bus
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
        private List<DriverItemModel> _drivers;

        public List<DriverItemModel> Drivers
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
        private string _summary;
        public string Summary
        {
            get => _summary;
            set
            {
                if (_summary != value)
                {
                    _summary = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Summary)));
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
        private bool _isPlan;
        public bool IsPlan
        {
            get => _isPlan;
            set
            {
                if (_isPlan != value)
                {
                    _isPlan = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPlan)));
                }
            }
        }
        private bool _isNotification;
        public bool IsNotification
        {
            get => _isNotification;
            set
            {
                if (_isNotification != value)
                {
                    _isNotification = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotification)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
