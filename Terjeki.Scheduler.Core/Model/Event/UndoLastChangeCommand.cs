namespace Terjeki.Scheduler.Core
{
    public class UndoLastChangeCommand : IRequest<bool>
    {
        public string EntityName { get; init; } = null!;
        public Guid EntityId { get; init; }
    }
}
