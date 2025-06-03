namespace Terjeki.Scheduler.Core
{
    public class BusItemModel
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string LicensePlateNumber { get; set; }
        public int CurrentMileage { get; set; }

        public override string ToString()
        {
            return $"{Brand} - {LicensePlateNumber}";
        }
    }
}
