namespace Terjeki.Scheduler.Core
{
    public class GetAuditHistoryQuery : IRequest<List<AuditHistoryModel>>
    {
        public string EntityName { get; init; } = null!;
        public Guid EntityId { get; init; }
    }
}
