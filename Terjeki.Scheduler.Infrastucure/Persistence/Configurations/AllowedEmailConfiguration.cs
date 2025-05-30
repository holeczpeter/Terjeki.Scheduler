using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Terjeki.Scheduler.Infrastucure.Persistence.Configurations
{
    internal class AllowedEmailConfiguration : IEntityTypeConfiguration<AllowedEmail>
    {
        public void Configure(EntityTypeBuilder<AllowedEmail> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasIndex(e => e.Email)
                    .IsUnique();
          
        }
    }
}

