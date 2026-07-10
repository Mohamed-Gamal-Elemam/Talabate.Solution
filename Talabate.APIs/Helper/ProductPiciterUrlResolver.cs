using AutoMapper;
using Talabate.APIs.Dtos;
using Talabate.Core.Entity;

namespace Talabate.APIs.Helper
{
    public class ProductPiciterUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPiciterUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseApiUrl"] } /{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
