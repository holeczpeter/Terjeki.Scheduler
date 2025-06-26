namespace Terjeki.Scheduler.Web.Forms
{
    public class CreateHolidayForm : INotifyPropertyChanged
    {

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
        private HolidayTypes _type;

        [Required(ErrorMessage = "A távollét megadása kötelező.")]
        public HolidayTypes Type
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


        public event PropertyChangedEventHandler PropertyChanged;
    }
}

