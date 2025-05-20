namespace Terjeki.Scheduler.Core
{
    public class BusModel
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }  
        public string Type { get; set; }
        public string Description { get; set; }
        public string LicensePlateNumber { get; set; }
        public CapacityModel Capacity { get; set; }
        public DriverModel Driver { get; set; }
        public int CurrentMileage { get; set; }
        public override bool Equals(object o)
        {
            var other = o as BusModel;

            return other?.Id == Id;
        }

        public override string ToString()
        {
            return $"{Brand} - {LicensePlateNumber}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
