using Terjeki.Scheduler.Core;

namespace Terjeki.Scheduler.Web.Components.Drivers
{
    public class UpdateDriverForm : INotifyPropertyChanged
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
        private Guid? _userId;

        
        public Guid? UserId
        {
            get => _userId;
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_userId)));
                }
            }
        }
       
        private string _driverName;
        public string DriverName
        {
            get => _driverName;
            set
            {
                if (_driverName != value)
                {
                    _driverName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_driverName)));
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
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
