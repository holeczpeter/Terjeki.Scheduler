using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100); 
        }
    }
}
