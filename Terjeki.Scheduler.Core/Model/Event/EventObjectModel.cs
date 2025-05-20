namespace Terjeki.Scheduler.Core
{
    public class EventObjectModel
    {
        public CapacityModel Capacity { get; set; }

        public BusModel Bus { get; set; }

        public List<DriverModel> Drivers { get; set; }

        public override string ToString()
        {
            var driverNames = Drivers != null
                ? string.Join(" / ", Drivers)
                : "N/A";

            return $"Sofőrök: {driverNames}";
        }
    }
    public class EventObjectModelComparer : IEqualityComparer<EventObjectModel>
    {
        public bool Equals(EventObjectModel x, EventObjectModel y)
        {
            return x?.Bus?.Id == y.Bus?.Id &&
                   x?.Capacity?.Capacity == y.Capacity?.Capacity;
        }

        public int GetHashCode(EventObjectModel obj)
        {
            if (obj == null)
                return 0;

            return HashCode.Combine(
                obj.Bus?.Id,
                obj.Capacity?.Capacity
            );
        }
    }
}
