namespace Terjeki.Scheduler.Core
{
    public class DriverItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BusItemModel Bus { get; set; }

        public override bool Equals(object o)
        {
            var other = o as DriverItemModel;

            return other?.Id == Id;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
