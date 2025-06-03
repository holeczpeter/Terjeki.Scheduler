namespace Terjeki.Scheduler.Core.Entities
{
    public class BaseEntity 
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Creator { get; set; } = string.Empty;
        public DateTime LastModified { get; set; } = DateTime.Now;
        public string LastModifier { get; set; } = string.Empty;
        [Timestamp]
        public byte[] RowVersion { get; set; } = default!;
        public EntityStatuses EntityStatus { get; set; } = EntityStatuses.Active;
        
    }
}
