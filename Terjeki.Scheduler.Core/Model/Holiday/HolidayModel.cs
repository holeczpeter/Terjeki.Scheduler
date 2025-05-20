namespace Terjeki.Scheduler.Core
{
    public class HolidayModel
    {
       
        
        public string Name { get; set; }
        
        public HolidayTypes Type { get; set; }

        public override bool Equals(object o)
        {
            var other = o as HolidayModel;

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
