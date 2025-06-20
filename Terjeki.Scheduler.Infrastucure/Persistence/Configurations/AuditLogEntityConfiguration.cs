using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class AuditLogEntityConfiguration : IEntityTypeConfiguration<AuditLogEntity>
    {
        public void Configure(EntityTypeBuilder<AuditLogEntity> builder)
        {

            builder.HasKey(c => c.Id);

            builder
                 .Property(e => e.Created)
                 .IsRequired();

            builder
                .Property(e => e.Creator)
                .HasMaxLength(200);

            builder
                .Property(e => e.LastModified)
                .IsRequired();

            builder
                .Property(e => e.LastModifier)
                .HasMaxLength(200);
            builder
              .Property(e => e.RowVersion)
              .IsRowVersion()
              .HasColumnType("rowversion");


        }
    }
}
