using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            BaseEntityConfiguration.Configure(builder);

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Summary)
                .IsRequired(false);
            builder.Property(e => e.Description)
                .IsRequired(false);

            builder.Property(e => e.StartDate)
                   .IsRequired();

            builder.Property(e => e.EndDate)
                   .IsRequired();

            builder.Property(e => e.Status)
                   .IsRequired();

            builder.Property(e => e.Type)
                   .IsRequired();

            builder.Property(e => e.ServiceType)
                   .IsRequired();

            builder.Property(e => e.HolidayType)
                   .IsRequired();

            builder.HasOne(e => e.Bus)
                   .WithMany(b => b.Events)
                   .HasForeignKey(e => e.BusId)
                   .OnDelete(DeleteBehavior.SetNull); 
          
        }

    }
}
