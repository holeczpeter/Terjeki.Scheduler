namespace Terjeki.Scheduler.Core
{
    public class AuditEntry
    {
        public int Id { get; set; }
        public string TableName { get; set; } = null!;
        public string KeyValues { get; set; } = null!;
        public string OldValues { get; set; } = null!;
        public string NewValues { get; set; } = null!;
        public Actions Action { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
    }
}
