using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class EventDriverConfiguration : IEntityTypeConfiguration<DriverEvent>
    {
        public void Configure(EntityTypeBuilder<DriverEvent> builder)
        {
            
            builder.HasKey(ed => new { ed.EventId, ed.DriverId });

           
            builder.HasOne(ed => ed.Event)
                   .WithMany(e => e.DriverEvents)
                   .HasForeignKey(ed => ed.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasOne(ed => ed.Driver)
                   .WithMany(d => d.DriverEvents)
                   .HasForeignKey(ed => ed.DriverId)
                   .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
