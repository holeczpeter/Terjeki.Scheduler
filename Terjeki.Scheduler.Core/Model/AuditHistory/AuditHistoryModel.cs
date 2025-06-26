using System.Text.Json;

namespace Terjeki.Scheduler.Core
{
    public class AuditHistoryModel
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid? UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public Dictionary<string, JsonElement> OldValues { get; set; } = new();
        public Dictionary<string, JsonElement> NewValues { get; set; } = new();
    }
}
