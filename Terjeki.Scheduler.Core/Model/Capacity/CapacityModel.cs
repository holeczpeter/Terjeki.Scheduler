using System.Xml.Linq;

namespace Terjeki.Scheduler.Core
{
    public class CapacityModel
    {
        public int Seats { get; set; }
        public int Extra { get; set; }

        public int Capacity => Seats + Extra;

        public override bool Equals(object o)
        {
            var other = o as CapacityModel;
            return other != null && Seats == other.Seats && Extra == other.Extra;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Seats, Extra);
        }

        public override string ToString()
        {
            return $"{Capacity}";
        }
    }

}
