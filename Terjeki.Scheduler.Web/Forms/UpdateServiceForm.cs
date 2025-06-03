using Terjeki.Scheduler.Core;

namespace Terjeki.Scheduler.Web.Components.Services
{
    public class UpdateServiceForm : INotifyPropertyChanged
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
        private ServiceTypes _type;

        [Required(ErrorMessage = "A szervíz megadása kötelező.")]
        public ServiceTypes Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
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
