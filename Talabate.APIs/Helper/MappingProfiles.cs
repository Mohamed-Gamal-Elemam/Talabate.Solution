using AutoMapper;
using Talabate.APIs.Dtos;
using Talabate.Core.Entity.Basket;
using Talabate.Core.Entity.Order_Agregate;
using Talabate.Core.Entity.Product;

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
            CreateMap<AddressDto,Address>();

        }
    }
}
