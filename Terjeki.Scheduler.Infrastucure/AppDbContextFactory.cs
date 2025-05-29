using Microsoft.EntityFrameworkCore.Design;

namespace Terjeki.Scheduler.Infrastucure
{

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {

        public AppDbContext CreateDbContext(string[] args)
        {
            var contextBuilder = new DbContextOptionsBuilder<AppDbContext>();
            contextBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=TerjekiScheduler;Integrated Security=SSPI;MultipleActiveResultSets=true;TrustServerCertificate=True;");

            return new AppDbContext(contextBuilder.Options);
        }
    }
}
