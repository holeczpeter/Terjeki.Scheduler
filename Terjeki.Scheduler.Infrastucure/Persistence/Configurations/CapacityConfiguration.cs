using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class CapacityConfiguration : IEntityTypeConfiguration<Capacity>
    {
        public void Configure(EntityTypeBuilder<Capacity> builder)
        {
            BaseEntityConfiguration.Configure(builder);
            builder.HasKey(d => d.Id);

           
        }
    }
}
