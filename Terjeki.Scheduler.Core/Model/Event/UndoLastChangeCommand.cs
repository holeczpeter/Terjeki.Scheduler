namespace Terjeki.Scheduler.Core
{
    public class UndoLastChangeCommand : IRequest
    {
        public string EntityName { get; init; } = null!;
        public Guid EntityId { get; init; }
    }
}
