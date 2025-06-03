namespace Terjeki.Scheduler.Core
{
    public class DriverEventModel
    {
        public DriverItemModel Driver { get; set; }
        public List<EventModel> Events { get; set; }
       
    }
    public class DriverEventModelComparer : IEqualityComparer<DriverEventModel>
    {
        public bool Equals(DriverEventModel x, DriverEventModel y)
        {
            return x?.Driver?.Id == y.Driver?.Id;
        }

        public int GetHashCode(DriverEventModel obj)
        {
            if (obj == null)
                return 0;

            return HashCode.Combine(
                obj.Driver?.Id
            );
        }
    }
}
