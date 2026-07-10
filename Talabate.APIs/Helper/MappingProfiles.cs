using AutoMapper;
using Talabate.APIs.Dtos;
using Talabate.Core.Entity;

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
        }
    }
}
