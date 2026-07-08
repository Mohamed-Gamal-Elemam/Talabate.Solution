using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;
using Talabate.Core.Specifications;

namespace Talabate.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenaricRepo<Product> _productRepo;

        public ProductsController(IGenaricRepo<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var spec=new ProductwithBrandCategorySpacification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            return Ok(products);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetAsync (int id)
        {
            var spec =new ProductwithBrandCategorySpacification();

            var product = await _productRepo.GetAWithSpecsync(spec);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
