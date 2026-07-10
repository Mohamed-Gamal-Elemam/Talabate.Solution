
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Talabate.APIs.Helper;
using Talabate.Core.IRepos;
using Talabate.Repository;
using Talabate.Repository.Data;

namespace Talabate.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Talabate.Infrastructer"));
            });
            builder.Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));

            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            #endregion



            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var services = Scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<StoreContext>();
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();


            try
            {
                await _dbcontext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(_dbcontext);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating the database.");
                
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
