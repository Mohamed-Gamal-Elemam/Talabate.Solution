using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabate.Core.Entity.Identity;
using Talabate.Infrastructer.Identity;
using Talabate.Repository.Genaric_Repoistory.Data;

namespace Talabate.APIs.Extantions
{
    public static class WebApplicationExtension
    {
        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreContext>();
            var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context);

                await identityContext.Database.MigrateAsync();
                await AppIdentityDataSeeding.SeedData(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();

                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}