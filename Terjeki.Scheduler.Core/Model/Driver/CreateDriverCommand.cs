namespace Terjeki.Scheduler.Core
{
    public class CreateDriverCommand : IRequest<DriverModel>
    {

        [Required]
        public DriverModel Driver { get; set; }
       
        public Guid? BusId { get; set; }

    }
}
