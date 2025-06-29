

using System.Text.Json;

namespace Terjeki.Scheduler.Infrastucure
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        public DbSet<AuditEntry> AuditEntries { get; set; } = null!;
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
            var userId = _currentUserService.GetUserId();
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.GetUserId();  
            var currentUsername = _currentUserService.GetUserName() ?? "Anonymous";
            var utcNow = DateTime.UtcNow;

            
            var entries = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e =>
                    (e.State == EntityState.Added ||
                     e.State == EntityState.Modified ||
                     e.State == EntityState.Deleted) &&
                    !e.Metadata.GetTableName()!.StartsWith("AspNet"));

            var auditEntries = new List<AuditEntry>();

            foreach (var entry in entries)
            {
                
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = utcNow;
                    entry.Entity.Creator = currentUsername;
                    entry.Entity.LastModified = utcNow;
                    entry.Entity.LastModifier = currentUsername;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModified = utcNow;
                    entry.Entity.LastModifier = currentUsername;
                }

                
                var audit = new AuditEntry
                {
                    TableName = entry.Metadata.GetTableName()!,
                    Action = entry.State switch
                    {
                        EntityState.Added => Actions.Insert,
                        EntityState.Modified => Actions.Update,
                        _ => Actions.Delete
                    },
                    Timestamp = utcNow,
                    UserId = userId,      
                    UserName = currentUsername,
                    KeyValues = JsonSerializer.Serialize(
                        entry.Properties
                             .Where(p => p.Metadata.IsPrimaryKey())
                             .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)),
                    OldValues = entry.State == EntityState.Added
                                    ? "{}"
                                    : JsonSerializer.Serialize(
                                        entry.Properties
                                             .Where(p => p.IsModified)
                                             .ToDictionary(p => p.Metadata.Name, p => p.OriginalValue)),
                    NewValues = entry.State == EntityState.Deleted
                                    ? "{}"
                                    : JsonSerializer.Serialize(
                                        entry.Properties
                                             .Where(p => p.IsModified || entry.State == EntityState.Added)
                                             .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue))
                };

                auditEntries.Add(audit);
            }

            if (auditEntries.Any())
                AuditEntries.AddRange(auditEntries);

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
