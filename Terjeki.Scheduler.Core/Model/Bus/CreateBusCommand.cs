namespace Terjeki.Scheduler.Core
{
    public class CreateBusCommand :  IRequest<BusModel>
    {
        [Required]
        public string Name { get; set; }
     
        public string Description { get; set; }

        [Required]
        public string LicensePlateNumber { get; set; }

        [Required]
        public CapacityModel Capacity { get; set; }

        public Guid? DriverId { get; set; } = new();

        public int CurrentMileage { get; set; }
    }
}
