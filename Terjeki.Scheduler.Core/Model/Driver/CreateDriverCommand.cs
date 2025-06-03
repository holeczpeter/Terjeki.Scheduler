namespace Terjeki.Scheduler.Core
{
    public class CreateDriverCommand : IRequest<DriverModel>
    {

        public Guid? BusId { get; set; }
        public Guid? DriverUserId { get; set; }
        public string? DriverName { get; set; }
    }
}
