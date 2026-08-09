using AutoMapper;
using Talabate.APIs.Dtos;
using Talabate.Core.Entity.Basket;
using Talabate.Core.Entity.Order_Agregate;
using OrderAddress= Talabate.Core.Entity.Order_Agregate.Address;
using Talabate.Core.Entity.Product;
using UserAddress=Talabate.Core.Entity.Identity.Address;

namespace Talabate.APIs.Helper
{
    public class MappingProfiles:Profile
    {
        
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(p=>p.Brand,o=>o.MapFrom(src=>src.Brand.Name))
                .ForMember(p=>p.Category,o=>o.MapFrom(src=>src.Category.Name))
                .ForMember(p=>p.PictureUrl,o=>o.MapFrom<ProductPiciterUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, OrderAddress>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(o => o.deliveryMethod, o => o.MapFrom(s => s.deliveryMethod.ShortName))
                .ForMember(o => o.deliveryMethodCost, o => o.MapFrom(s => s.deliveryMethod.Cost));

            CreateMap<UserAddress, AddressDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(o => o.ProductID, o => o.MapFrom(s => s.Product.ProductID))
                .ForMember(o => o.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(o => o.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
                .ForMember(o=>o.PictureUrl,o=>o.MapFrom<OrderItemPiciterUrlResolver>());

        }
    }
}
