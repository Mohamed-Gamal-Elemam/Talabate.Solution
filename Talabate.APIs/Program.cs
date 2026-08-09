using Talabate.APIs.Extantions;
using Talabate.APIs.Middleware;

namespace Talabate.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Services

            builder.Services.AddControllers().AddNewtonsoftJson(options
                =>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            #endregion

            var app = builder.Build();

            await app.SeedDatabaseAsync();

            app.UseMiddleware<ExcaptionHandelarMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseStatusCodePagesWithReExecute("/errors/{0}");

                app.UseSwagger();
                app.UseSwaggerUI();

                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}