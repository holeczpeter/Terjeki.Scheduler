namespace Terjeki.Scheduler.Web.Forms
{
    public class UpdateHolidayForm: CreateHolidayForm
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
