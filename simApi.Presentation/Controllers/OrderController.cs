using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simApi.Base;
using simApi.Presentation.ActionFilters;
using simApi.Schema;
using simApi.Service;

namespace simApi.Presentation.Controllers
{
    [Route("simapi/v1/[controller]")]
    [ApiController]
    
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpGet]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse<List<OrderResponse>>> GetAll()
        {
            var orderList =  orderService.GetAll();
            return orderList;
        }
        [HttpGet("ActiveOrder")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetActiveOrder()
        {
            var orderList = await orderService.GetOrdersIsActive();
            return orderList;
        }
        [HttpGet("PassiveOrder")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetPasifOrder()
        {
            var orderList =await orderService.GetOrdersIsPasif();
            return orderList;
        }
        [HttpGet("GetUserOrder/{userId}")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetUserOrder([FromRoute] string userId)
        {
            var orderList =await orderService.GetUserOrders(userId);
            return orderList;
        }

        [HttpGet("GetOrderNumber/orderNumber")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrderNumber([FromQuery] string orderNumber)
        {
            var orderList = await orderService.GetOrdersNumber(orderNumber);
            return orderList;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse<OrderResponse> GetById([FromRoute] int id)
        {
            var order = orderService.GetById(id);
            return order;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Post([FromBody] OrderRequest request)
        {
            return orderService.Insert(request);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Put([FromRoute] int id, [FromBody] OrderRequest request)
        {
            return orderService.Update(id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public ApiResponse Delete([FromRoute] int id)
        {
            return orderService.Delete(id);
        }
    }
}

