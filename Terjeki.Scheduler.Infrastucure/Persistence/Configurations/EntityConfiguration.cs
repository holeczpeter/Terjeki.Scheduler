using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public static class EntityConfiguration
    {
        public static void ConfigureEntityPart<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            
            builder
                .Property(e => e.Created)
                .IsRequired();
            builder
                .Property(e => e.Creator);
            builder
                .Property(e => e.LastModified)
                .IsRequired();
            builder
                .Property(e => e.LastModifier);
            builder
                .HasIndex(e => e.EntityStatus);
            
            builder
                .Property(e => e.RowVersion)
                .IsRowVersion();
        }
    }
}
