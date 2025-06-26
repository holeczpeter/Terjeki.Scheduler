using System.Text.Json;

namespace Terjeki.Scheduler.Application
{
    internal class UndoLastChangeCommandHandler : IRequestHandler<UndoLastChangeCommand, bool>
    {
        private readonly AppDbContext _db;
        public UndoLastChangeCommandHandler(AppDbContext db) => _db = db;

        public async Task<bool> Handle(UndoLastChangeCommand request, CancellationToken cancellationToken)
        {
            // Build JSON key for matching
            var keyJson = JsonSerializer.SerializeToElement(new Dictionary<string, object> { { "Id", request.EntityId } });

            // Get the most recent audit entry for this entity
            var audit = await _db.AuditEntries
                .Where(a => a.TableName == request.EntityName
                         && a.KeyValues == keyJson.GetRawText())
                .OrderByDescending(a => a.Timestamp)
                .FirstOrDefaultAsync(cancellationToken);

            if (audit == null)
                throw new KeyNotFoundException($"No audit entries found for {request.EntityName} with Id {request.EntityId}");

            var oldValues = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(audit.OldValues)!;
            var entityType = _db.Model.GetEntityTypes()
                .First(et => et.GetTableName() == audit.TableName)
                .ClrType;
            var entity = await _db.FindAsync(entityType, new object[] { request.EntityId }, cancellationToken);
            if (entity == null)
                throw new InvalidOperationException($"Entity {request.EntityName} with Id {request.EntityId} not found");

            // Apply old values
            foreach (var kv in oldValues)
            {
                var prop = entityType.GetProperty(kv.Key);
                if (prop == null || !prop.CanWrite) continue;
                var value = JsonSerializer.Deserialize(kv.Value.GetRawText(), prop.PropertyType);
                prop.SetValue(entity, value);
            }

            // Save changes (will generate a new audit entry for the undo)
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

       
    }
}
