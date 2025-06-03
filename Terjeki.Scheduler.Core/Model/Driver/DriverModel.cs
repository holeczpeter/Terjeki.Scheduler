namespace Terjeki.Scheduler.Core
{
    public class DriverModel
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public BusItemModel Bus { get; set; }

        public override bool Equals(object o)
        {
            var other = o as DriverModel;

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
