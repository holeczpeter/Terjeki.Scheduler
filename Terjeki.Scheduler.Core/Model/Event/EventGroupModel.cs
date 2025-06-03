namespace Terjeki.Scheduler.Core
{
    public class EventGroupModel
    {
        
        public CapacityModel Capacity { get; set; }

        public BusItemModel Bus { get; set; }

        public List<EventModel> Events { get; set; }

       
    }
}

