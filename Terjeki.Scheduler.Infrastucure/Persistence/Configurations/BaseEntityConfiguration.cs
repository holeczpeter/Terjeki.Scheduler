using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public static class BaseEntityConfiguration
    {
        /// <summary>
        /// Egy segédfüggvény, amit minden IEntityTypeConfiguration-ban 
        /// használhatsz, hogy a BaseEntity általános mezőit beállítsd.
        /// (például Created, Creator, LastModified, LastModifier, EntityStatus, RowVersion).
        /// </summary>
        public static void Configure<TEntity>(EntityTypeBuilder<TEntity> builder)
            where TEntity : BaseEntity
        {
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
                .HasIndex(e => e.EntityStatus);

            builder
                .Property(e => e.RowVersion)
                .IsRowVersion()      
                .HasColumnType("rowversion");
        }
    }
}
