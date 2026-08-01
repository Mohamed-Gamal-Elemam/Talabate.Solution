using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabate.APIs.Dtos;
using Talabate.APIs.Error;
using Talabate.Core.Entity.Order_Agregate;
using Talabate.Core.IServices;

namespace Talabate.APIs.Controllers
{
 
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService ,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(Order),statusCode:200)]
        [ProducesResponseType(typeof(ApiResponse),statusCode:400)]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var Address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, Address);
            if (order is null) return BadRequest(new ApiResponse(400));

            return Ok(order);
        }
    }
}
