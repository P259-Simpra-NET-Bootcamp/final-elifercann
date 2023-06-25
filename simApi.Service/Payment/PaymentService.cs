using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Schema;

namespace simApi.Service;

public class PaymentService : BaseService<Payment, PaymentRequest, PaymentResponse>, IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    public PaymentService(IUnitOfWork unitOfWork, IMapper mapper,IHttpContextAccessor contextAccessor,UserManager<AppUser> userManager) : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
    }

    public bool MakePayment(PaymentRequest request)
    {
        try
        {
            var httpContext = _contextAccessor.HttpContext;
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;

            var appUser = _userManager.FindByIdAsync(userId).Result;

            var payment = new Payment
            {
                UserId = userId,
                OrderNumber = request.OrderNumber, 
                TotalAmount = request.TotalAmount,
                CouponAmount = request.CouponAmount,
                PointAmount = request.PointAmount,
                CouponCode = request.CouponCode,
                PaymentDate = DateTime.Now
            };
            request.PointAmount += appUser.DigitalWallet;
            _unitOfWork.Repository<Payment>().Insert(payment);
            _unitOfWork.Complete();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}

