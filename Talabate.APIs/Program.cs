using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabate.APIs.Error;
using Talabate.APIs.Helper;
using Talabate.APIs.Middleware;
using Talabate.Application.Services;
using Talabate.Core.IRepos;
using Talabate.Core.IServices;
using Talabate.Core.IUnitOfWork;
using Talabate.Infrastructer.BasketRepo;
using Talabate.Infrastructer.Genaric_Repoistory;
using Talabate.Infrastructer.UnitOfWork;
using Talabate.Repository.Genaric_Repoistory.Data;

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
            builder.Services.AddScoped(typeof(IBasketRepo),typeof(BasketRepo) );
            builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(IOrderService),typeof(OrderService));

            builder.Services.AddScoped<IConnectionMultiplexer>((servicesProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Talabate.Infrastructer"));
            });
            builder.Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));

            builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfiles));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(e => e.ErrorMessage)
                                                       .ToList();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                return new BadRequestObjectResult(response);

                };

            });



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

            app.UseMiddleware<ExcaptionHandelarMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseStatusCodePagesWithReExecute("/errors/{0}"); 
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();



            app.MapControllers();

            app.Run();
        }
    }
}
