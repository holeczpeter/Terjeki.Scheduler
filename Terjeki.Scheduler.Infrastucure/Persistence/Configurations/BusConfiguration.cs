using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class BusConfiguration : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            BaseEntityConfiguration.Configure(builder);
            builder.HasKey(b => b.Id);
            builder.Property(b => b.LicensePlateNumber).IsRequired().HasMaxLength(20);
            builder.Property(b => b.Brand).HasMaxLength(50);
            builder.Property(b => b.Type).HasMaxLength(50);

            builder.HasOne(e => e.Capacity)
                  .WithMany(b => b.Buses)
                  .HasForeignKey(e => e.CapacityId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Events)
               .WithOne(e => e.Bus)
               .HasForeignKey(e => e.BusId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Driver)
                    .WithOne(d => d.Bus)
                    .HasForeignKey<Bus>(b => b.DriverId)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
