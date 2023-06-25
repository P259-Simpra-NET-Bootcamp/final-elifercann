using AutoMapper;
using Serilog;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Schema;

namespace simApi.Service;

public class CouponService : BaseService<Coupon, CouponRequest, CouponResponse>, ICouponService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CouponService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override ApiResponse<List<CouponResponse>> GetAll()
    {
        try
        {
            var entity = _unitOfWork.Repository<Coupon>().GetAll();
            if (entity is null)
            {
                return new ApiResponse<List<CouponResponse>>("Record not found");
            }

            var mapped = _mapper.Map<List<Coupon>, List<CouponResponse>>(entity);
            return new ApiResponse<List<CouponResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CouponResponse>>(ex.Message);
        }
    }
    public async Task<ApiResponse<CouponResponse>> GetByCouponCode(string code)
    {
        try
        {
            var couponcode = await Task.FromResult(_unitOfWork.Repository<Coupon>().GetAll().Where(x => x.Code == code).FirstOrDefault());
            if (couponcode is null)
            {
                return new ApiResponse<CouponResponse>("There is no coupon for this code");
            }

            var mapped = _mapper.Map<Coupon,CouponResponse>(couponcode);
            return new ApiResponse<CouponResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CouponResponse>(ex.Message);
        }
    }
    public override ApiResponse<CouponResponse> GetById(int id)
    {
        try
        {
            var entity = _unitOfWork.Repository<Coupon>().GetById(id);
            if (entity is null)
            {
                return new ApiResponse<CouponResponse>("There is no coupon for this id");
            }

            var mapped = _mapper.Map<Coupon, CouponResponse>(entity);
            return new ApiResponse<CouponResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CouponResponse>(ex.Message);
        }
    }
    public async Task<ApiResponse<IEnumerable<CouponResponse>>> GetCouponActive()
    {
        try
        {
            var activeOrder = await Task.FromResult(_unitOfWork.Repository<Coupon>().GetAll().Where(x => x.IsActive == true));
            if (activeOrder is null)
            {
                return new ApiResponse<IEnumerable<CouponResponse>>("No active coupons");
            }
            var mapped = _mapper.Map<IEnumerable<Coupon>, IEnumerable<CouponResponse>>(activeOrder);
            return new ApiResponse<IEnumerable<CouponResponse>>(mapped);
        }
        catch (Exception ex)
        {

            return new ApiResponse<IEnumerable<CouponResponse>>(ex.Message);
        }
    }
    public async Task<ApiResponse<IEnumerable<CouponResponse>>> GetCouponPasif()
    {
        try
        {
            var activeOrder = await Task.FromResult(_unitOfWork.Repository<Coupon>().GetAll().Where(x => x.IsActive == false|| x.ExpiryDate<DateTime.UtcNow));
            if (activeOrder is null)
            {
                return new ApiResponse<IEnumerable<CouponResponse>>("No passive coupons");
            }
            var mapped = _mapper.Map<IEnumerable<Coupon>, IEnumerable<CouponResponse>>(activeOrder);
            return new ApiResponse<IEnumerable<CouponResponse>>(mapped);
        }
        catch (Exception ex)
        {

            return new ApiResponse<IEnumerable<CouponResponse>>(ex.Message);
        };
    }
    public override ApiResponse Insert(CouponRequest request)
    {
        try
        {
            var mapped = _mapper.Map<CouponRequest, Coupon>(request);
            _unitOfWork.Repository<Coupon>().Insert(mapped);
            _unitOfWork.Complete();
            return new ApiResponse();
        }
        catch (Exception ex)
        {

            return new ApiResponse(ex.Message);
        }
    }
   
}
