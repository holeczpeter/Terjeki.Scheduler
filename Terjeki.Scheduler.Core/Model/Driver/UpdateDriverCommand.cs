using System.Globalization;

namespace Terjeki.Scheduler.Core
{
    public class UpdateDriverCommand : IRequest<DriverModel>
    {
        public Guid Id { get; set; }

        public Guid? BusId { get; set; }

        public Guid? UserId { get; set; }

        public string? DriverName { get; set; }
    }
}
