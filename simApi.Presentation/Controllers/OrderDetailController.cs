using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simApi.Base;
using simApi.Schema;
using simApi.Service;

namespace simApi.Presentation.Controllers
{
    [Route("simapi/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
        }
        [HttpGet]
        public ApiResponse<List<OrderDetailResponse>> GetAll()
        {
            var orderDetailList =  orderDetailService.GetAll();
            return orderDetailList;
        }

        [HttpGet("{id}")]
        public ApiResponse<OrderDetailResponse> GetById(int id)
        {
            var orderDetail = orderDetailService.GetById(id);
            return orderDetail;
        }
       
       
    }
}

