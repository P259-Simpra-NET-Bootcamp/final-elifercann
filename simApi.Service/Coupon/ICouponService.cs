using simApi.Base;
using simApi.Data;
using simApi.Schema;

namespace simApi.Service;

public interface ICouponService:IBaseService<Coupon,CouponRequest,CouponResponse>
{
    Task<ApiResponse<IEnumerable<CouponResponse>>> GetCouponActive();
    Task<ApiResponse<IEnumerable<CouponResponse>>> GetCouponPasif();
    Task<ApiResponse<CouponResponse>> GetByCouponCode(string code);
}
