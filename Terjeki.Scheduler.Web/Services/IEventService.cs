
namespace Terjeki.Scheduler.Web.Services
{
    public interface IEventService
    {
        Task<EventModel> Create(CreateEventCommand command, CancellationToken cancellationToken = default);
        Task<EventModel> Update(UpdateEventCommand command, CancellationToken cancellationToken = default);
        Task<bool> Undo(UndoLastChangeCommand command, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeleteEventCommand command, CancellationToken cancellationToken = default);
        Task<EventModel> Get(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<DriverEventModel>> GetDriverEvents(DateTime from, DateTime to, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventModel>> GetEvents(CancellationToken cancellationToken = default);
        Task<IEnumerable<EventGroupModel>> GetGrouped(DateTime from, DateTime to, CancellationToken cancellationToken = default);
       
    }
}