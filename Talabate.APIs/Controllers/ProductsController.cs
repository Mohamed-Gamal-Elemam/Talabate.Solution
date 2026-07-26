using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.APIs.Dtos;
using Talabate.APIs.Error;
using Talabate.APIs.Helper;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;
using Talabate.Core.Specifications;
using Talabate.Core.Specifications.ProductSpecs;

namespace Talabate.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenaricRepo<Product> _productRepo;
        public IMapper _Mapper { get; }
        public IGenaricRepo<ProductBrand> _Brandrepo { get; }
        public IGenaricRepo<ProductCategory> _Categpryrepo { get; }

        public ProductsController(IGenaricRepo<Product> productRepo ,IMapper mapper,IGenaricRepo<ProductBrand> brandrepo,IGenaricRepo<ProductCategory> categpryrepo)
        {
            _productRepo = productRepo;
            _Mapper = mapper;
            _Brandrepo = brandrepo;
            _Categpryrepo = categpryrepo;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllAsync([FromQuery] ProductSpecPrams specPrams)
        {
            var spec=new ProductwithBrandCategorySpacification(specPrams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var data= _Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var countSpec = new ProductsWithFilterationForCountSpec(specPrams);
            var count = await _productRepo.CountAsync(countSpec);
            return Ok(new Pagination<ProductToReturnDto>(specPrams.PageIndex, specPrams.PageSize,count, data));
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetAsync (int id)
        {
            var spec =new ProductwithBrandCategorySpacification(id);

            var product = await _productRepo.GetAWithSpecsync(spec);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_Mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _Brandrepo.GetAllAsync();
            return Ok(Brands);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> Getcategories() 
        {
            var Categories = await _Categpryrepo.GetAllAsync();
            return Ok(Categories);
        }
          
        
    }
}
