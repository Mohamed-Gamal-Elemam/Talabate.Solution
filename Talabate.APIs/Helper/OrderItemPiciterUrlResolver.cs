using AutoMapper;
using AutoMapper.Execution;
using Talabate.APIs.Dtos;
using Talabate.Core.Entity.Order_Agregate;

namespace Talabate.APIs.Helper
{
    public class OrderItemPiciterUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPiciterUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_configuration["BaseApiUrl"]} /{source.Product.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
