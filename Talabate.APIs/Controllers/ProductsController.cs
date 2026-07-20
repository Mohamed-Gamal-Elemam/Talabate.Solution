using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.APIs.Dtos;
using Talabate.APIs.Error;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;
using Talabate.Core.Specifications;

namespace Talabate.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenaricRepo<Product> _productRepo;
        public IMapper _Mapper { get; }

        public ProductsController(IGenaricRepo<Product> productRepo ,IMapper mapper)
        {
            _productRepo = productRepo;
            _Mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllAsync()
        {
            var spec=new ProductwithBrandCategorySpacification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(_Mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetAsync (int id)
        {
            var spec =new ProductwithBrandCategorySpacification();

            var product = await _productRepo.GetAWithSpecsync(spec);
            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_Mapper.Map<Product, ProductToReturnDto>(product));
        }
    }
}
