namespace Terjeki.Scheduler.Core.Entities
{
    public class Capacity : BaseEntity
    {
        public int Seats { get; set; }
        public int Extra { get; set; }
        public ICollection<Bus> Buses { get; set; } = new HashSet<Bus>();
    }
}
