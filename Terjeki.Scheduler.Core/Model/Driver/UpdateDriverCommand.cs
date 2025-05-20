using System.Globalization;

namespace Terjeki.Scheduler.Core
{
    public class UpdateDriverCommand : IRequest<DriverModel>
    {
        public Guid Id { get; set; }

        public BusModel Bus { get; set; }
    }
}
