

namespace Terjeki.Scheduler.Infrastucure
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly ICurrentUserService _currentUserService;

        public DbSet<Event> Events { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Capacity> Capacities { get; set; }
        public DbSet<DriverEvent> DriverEvents { get; set; }
        public DbSet<AllowedEmail> AllowedEmails { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
        public override int SaveChanges()
        {
            var currentUsername = _currentUserService.GetUserName();
            var utcNow = DateTime.UtcNow;
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Creator = currentUsername;
                    entry.Entity.Created = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifier = currentUsername;
                    entry.Entity.LastModified = utcNow;
                }
            }

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var currentUsername = _currentUserService.GetUserName();
            var utcNow = DateTime.UtcNow;
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Creator = currentUsername;
                    entry.Entity.Created = utcNow;
                    entry.Entity.LastModifier = currentUsername;
                    entry.Entity.LastModified = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifier = currentUsername;
                    entry.Entity.LastModified = utcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        
    }
}
