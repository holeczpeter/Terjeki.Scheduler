namespace Terjeki.Scheduler.Core
{
    public class UpdateServiceCommand : CreateServiceCommand, IRequest<EventModel>
    {
        public Guid Id { get; set; }
    }
}
