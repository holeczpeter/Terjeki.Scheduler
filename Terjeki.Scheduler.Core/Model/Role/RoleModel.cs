namespace Terjeki.Scheduler.Core
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RoleTypes Type { get; set; }

        public override bool Equals(object o)
        {
            var other = o as RoleModel;

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
