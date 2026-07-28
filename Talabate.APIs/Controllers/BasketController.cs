using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.APIs.Dtos;
using Talabate.APIs.Error;
using Talabate.Core.Entity.Basket;
using Talabate.Core.IRepos;

namespace Talabate.APIs.Controllers
{
   
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepo basketRepo,IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket=await _basketRepo.GetBasketAsync(id);
            return Ok(basket==null ?new CustomerBasket(id): basket);
        }


        [HttpPost]
         public async Task<ActionResult<CustomerBasket>> Update (CustomerBasketDto basket)
        {
            var mappedBasket=_mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var CreatedOrUpdated = await _basketRepo.UpdateBasketAsync(mappedBasket);
            if (CreatedOrUpdated is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdated);

        }


        [HttpDelete]
        public async Task<ActionResult<bool>> Delete (string id)
        {
            return await _basketRepo.DeleteBasketAsync(id);

        }
    }
}
