using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    internal class AuditEntriesConfiguration : IEntityTypeConfiguration<AuditEntry>
    {
        public void Configure(EntityTypeBuilder<AuditEntry> builder)
        {
            
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TableName)
                  .IsRequired()
                  .HasMaxLength(128);
            builder.Property(e => e.KeyValues)
                  .IsRequired();
            builder.Property(e => e.OldValues)
                   .IsRequired();
            builder.Property(e => e.NewValues)
                  .IsRequired();
            builder.Property(e => e.Action)
                  .IsRequired()
                  .HasMaxLength(16);
            builder.Property(e => e.Timestamp)
                  .IsRequired();
            builder.Property(e => e.UserId)
                   .IsRequired(false);
            builder.HasIndex(e => new { e.TableName, e.Timestamp });

        }
    }
}
