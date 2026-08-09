using Microsoft.AspNetCore.Mvc;
using Talabate.APIs.Error;
using Talabate.APIs.Helper;
using Talabate.Application.Services;
using Talabate.Core.IRepos;
using Talabate.Core.IServices;
using Talabate.Core.IUnitOfWork;
using Talabate.Infrastructer.BasketRepo;
using Talabate.Infrastructer.Genaric_Repoistory;
using Talabate.Infrastructer.UnitOfWork;

namespace Talabate.APIs.Extantions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketRepo), typeof(BasketRepo));
            services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAuthServices), typeof(AuthServices));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductServices, ProductServices>();

            services.AddAutoMapper(cfg => { }, typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var response = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}