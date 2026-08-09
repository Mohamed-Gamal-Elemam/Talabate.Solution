using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabate.APIs.Dtos;
using Talabate.APIs.Error;
using Talabate.Core.Entity.Order_Agregate;
using Talabate.Core.IServices;

namespace Talabate.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(OrderToReturnDto), statusCode: 200)]
        [ProducesResponseType(typeof(ApiResponse), statusCode: 400)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var Address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, Address);
            if (order is null) return BadRequest(new ApiResponse(400));

            return Ok(_mapper.Map<Order,OrderToReturnDto>(order));
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(BuyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(orders));
        }


        [ProducesResponseType(typeof(OrderToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByByIdForUser(int id )
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order =await _orderService.GetOrderByIdForUserAsync(id, BuyerEmail);
            if (order is null) return NotFound(new ApiResponse(404));   
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }


        [HttpGet("deliveryMethod")]

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetSeliveryMethods()
        {
            var DeliverMethods=await _orderService.GetDeliveryMethods();
            return Ok(DeliverMethods);
        }

    }
}
