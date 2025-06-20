using System.Text.Json;

namespace Terjeki.Scheduler.Application
{
    public class GetAuditHistoryQueryHandler : IRequestHandler<GetAuditHistoryQuery, List<AuditHistoryModel>>
    {
        private readonly AppDbContext _db;
        public GetAuditHistoryQueryHandler(AppDbContext db) => _db = db;

        public async Task<List<AuditHistoryModel>> Handle(GetAuditHistoryQuery request, CancellationToken cancellationToken)
        {
            var keyJson = JsonSerializer.SerializeToElement(new Dictionary<string, object> { { "Id", request.EntityId } });
            var entries = await _db.AuditEntries
                .Where(a => a.TableName == request.EntityName
                         && a.KeyValues == keyJson.GetRawText())
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync(cancellationToken);

            return entries.Select(a => new AuditHistoryModel
            {
                Id = a.Id,
                Timestamp = a.Timestamp,
                UserId = a.UserId,
                OldValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(a.OldValues)!,
                NewValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(a.NewValues)!
            }).ToList();
        }
    }
}
