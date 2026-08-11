using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabate.APIs.Dtos;
using Talabate.APIs.Error;
using Talabate.APIs.Helper;
using Talabate.Core.Entity.Product;
using Talabate.Core.IServices;
using Talabate.Core.Specifications.ProductSpecs;

namespace Talabate.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IProductServices _productServices;

        public IMapper _Mapper { get; }
       

        public ProductsController(IMapper mapper ,IProductServices productServices)
        {
            
            _Mapper = mapper;
           _productServices = productServices;
        }
        [CachedAttribute(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllAsync([FromQuery] ProductSpecPrams specPrams)
        {

            var products =await _productServices.GetProductsAsync(specPrams);
            var data= _Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var count =await _productServices.GetCountAsync(specPrams);
            return Ok(new Pagination<ProductToReturnDto>(specPrams.PageIndex, specPrams.PageSize,count, data));
        }
        [HttpGet("{id}")]
        [CachedAttribute(600)]
        public async Task<ActionResult<ProductToReturnDto>> GetAsync (int id)
        {
            

            var product = await _productServices.GetProductByIdAsync(id);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_Mapper.Map<Product, ProductToReturnDto>(product));
        }
        [CachedAttribute(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _productServices.GetProductBrandsAsync();
            return Ok(Brands);
        }
        [CachedAttribute(600)]
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> Getcategories() 
        {
            var Categories = await _productServices.GetProductCategorysAsync();
            return Ok(Categories);
        }
          
        
    }
}
