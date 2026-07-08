using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;

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
            var products = await _productRepo.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetAsync (int id)
        {
            var product = await _productRepo.GetAsync(id);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
