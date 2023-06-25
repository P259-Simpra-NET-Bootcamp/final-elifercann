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
    public class CouponController : ControllerBase
    {
        private readonly ICouponService couponService;

        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse<List<CouponResponse>> GetAll()
        {
            var couponList = couponService.GetAll();
            return couponList;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse<CouponResponse> GetById([FromRoute] int id)
        {
            var coupon = couponService.GetById(id);
            return coupon;
        }

        [HttpGet("GetCode")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse<CouponResponse>> GetByCouponCode([FromQuery] string code)
        {
            var coupon =await couponService.GetByCouponCode(code);
            return coupon;
        }
        [HttpGet("ActiveCoupon")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse<IEnumerable<CouponResponse>>> GetActiveCoupon()
        {
            var coupon = await couponService.GetCouponActive();
            return coupon;
        }
        [HttpGet("PassiveCoupon")]
        [Authorize(Roles = "admin,user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse<IEnumerable<CouponResponse>>> GetPassiveCoupon()
        {
            var coupon = await couponService.GetCouponPasif();
            return coupon;
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Post([FromBody] CouponRequest request)
        {
            return couponService.Insert(request);
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Put([FromRoute] int id, [FromBody] CouponRequest request)
        {
            return couponService.Update(id, request);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ApiResponse Delete([FromRoute] int id)
        {
            return couponService.Delete(id);
        }
    }
}
