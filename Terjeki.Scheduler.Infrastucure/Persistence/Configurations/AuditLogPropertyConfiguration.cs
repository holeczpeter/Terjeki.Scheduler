using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class AuditLogPropertyConfiguration : IEntityTypeConfiguration<AuditLogProperty>
    {
        public void Configure(EntityTypeBuilder<AuditLogProperty> builder)
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

            builder
               .HasOne(c => c.AuditLogEntity)
               .WithMany(g => g.AuditLogProperties)
               .HasForeignKey(s => s.AuditEntityId)
               .HasConstraintName("FK_AUDITANDITEMS_CONNECTION")
               .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
