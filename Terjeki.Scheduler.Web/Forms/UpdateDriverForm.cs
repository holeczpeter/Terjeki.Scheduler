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
        private string _name;

        [Required]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_name)));
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
