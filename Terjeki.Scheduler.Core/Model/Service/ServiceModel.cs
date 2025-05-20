namespace Terjeki.Scheduler.Core
{
    public class ServiceModel
    {
        
        public string Name { get; set; }
        public ServiceTypes Type { get; set; }

        public override bool Equals(object o)
        {
            var other = o as ServiceModel;

            return other?.Type == Type;
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
