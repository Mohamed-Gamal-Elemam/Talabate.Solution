using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabate.Core.Entity.Identity;
using Talabate.Infrastructer.Identity;
using Talabate.Repository.Genaric_Repoistory.Data;
using Microsoft.IdentityModel.Tokens;

namespace Talabate.APIs.Extantions
{
    public static class InfrastructureServicesExtension
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Talabate.Infrastructer"));
            });

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                       .UseSqlServer(
                            configuration.GetConnectionString("IdentityConnection"),
                            x => x.MigrationsAssembly("Talabate.Infrastructer"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connection =
                    configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["JWT:ValidIssuer"],
                            ValidAudience = configuration["JWT:ValidAudience"],
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                            ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationDays"]))

                        };
                    });

            return services;
        }
    }
}