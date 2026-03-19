using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var retries = 10;

            while (retries > 0)
            {
                try
                {
                    context.Database.Migrate();
                    break;
                }
                catch
                {
                    retries--;
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
