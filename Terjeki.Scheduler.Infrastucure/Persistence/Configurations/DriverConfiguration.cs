using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            BaseEntityConfiguration.Configure(builder);
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(d => d.User)
               .WithOne()          
               .HasForeignKey<Driver>(d => d.UserId)
               .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
