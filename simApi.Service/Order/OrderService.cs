using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace simApi.Service;

public class OrderService : BaseService<Order, OrderRequest, OrderResponse>, IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IPaymentService _paymentService;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, IPaymentService paymentService) : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _paymentService = paymentService;
    }

    public override ApiResponse<List<OrderResponse>> GetAll()
    {
        try
        {
            var entity = _unitOfWork.Repository<Order>().GetAll();
            if (entity is null)
            {
                return new ApiResponse<List<OrderResponse>>("Record not found");
            }

            var mapped = _mapper.Map<List<Order>, List<OrderResponse>>(entity);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<OrderResponse>>(ex.Message);
        }
    }
    public override ApiResponse<OrderResponse> GetById(int id)
    {
        try
        {
            var entity = _unitOfWork.Repository<Order>().GetByIdWithInclude(id, "OrderDetails");
            if (entity is null)
            {
                return new ApiResponse<OrderResponse>("Record not found");
            }

            var mapped = _mapper.Map<Order, OrderResponse>(entity);
            return new ApiResponse<OrderResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<OrderResponse>(ex.Message);
        }
    }
    public override ApiResponse Delete(int Id)
    {
        _unitOfWork.Repository<Order>().DeleteWith(Id);
        return new ApiResponse { Success = true, Message = "Deletion successful" };

    }

    public override ApiResponse Insert(OrderRequest request)
    {
        try
        {
            var httpContext = _contextAccessor.HttpContext;
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var appUser = _userManager.FindByIdAsync(userId).Result;

            // Order nesnesini oluşturma
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                IsActive = true,
                CouponCode = request.CouponCode,
                OrderNumber = GenerateOrderNumber()
        };

            var earnedPoints = 0;
            var maxTotalScore = 0;

            foreach (var detail in request.OrderDetails)
            {
                var product = _unitOfWork.Repository<Product>().Where(p => p.Id == detail.ProductId).FirstOrDefault();

                if (product != null)
                {
                    var orderDetail = new OrderDetail
                    {
                        Quantity = detail.Quantity,
                        Price = product.Price * detail.Quantity,
                        ProductId = detail.ProductId,
                    };

                    _unitOfWork.Repository<OrderDetail>().Insert(orderDetail);
                    order.BasketAmount += Convert.ToInt32(orderDetail.Price);

                    maxTotalScore += product.MaxTotalScore;

                    var pointsEarned = product.Price * product.WinEarnPoints / 100 * detail.Quantity;
                    earnedPoints += Convert.ToInt32(pointsEarned);
                }
            }

            if (!string.IsNullOrEmpty(request.CouponCode))
            {
                var coupon = _unitOfWork.Repository<Coupon>().Where(x => x.Code == request.CouponCode).FirstOrDefault();
                if (coupon != null && coupon.IsActive && coupon.ExpiryDate >= DateTime.Now)
                {
                    // Kuponun toplam fiyattan düşülmesi
                    order.BasketAmount -= coupon.Amount;
                    order.CouponAmount = coupon.Amount;
                    order.CouponCode = request.CouponCode;
                    coupon.IsActive = false;
                }
            }

          
            if (earnedPoints > maxTotalScore)
            {
                earnedPoints = maxTotalScore;
            }

            if (appUser != null && appUser.DigitalWallet > 0)
            {
                if (appUser.DigitalWallet >= order.BasketAmount)
                {
                    appUser.DigitalWallet -= order.BasketAmount;
                    order.BasketAmount = 0;
                }
                else
                {
                    order.BasketAmount -= appUser.DigitalWallet;
                    appUser.DigitalWallet = 0;
                }
            }

         
            order.PointAmount = earnedPoints;

            if (appUser != null)
            {
                // Kullanıcının puanının güncellenmesi
                appUser.DigitalWallet += earnedPoints;
                _userManager.UpdateAsync(appUser).GetAwaiter().GetResult();
            }

            // Siparişin kaydedilmesi
            _unitOfWork.Repository<Order>().Insert(order);
            _unitOfWork.Complete();

            // PaymentService'in MakePayment metodu çağrılıyor
            var paymentRequest = new PaymentRequest
            {
                OrderNumber = order.OrderNumber,
                TotalAmount = order.BasketAmount,
                CouponAmount = order.CouponAmount,
                PointAmount = order.PointAmount,
                CouponCode = order.CouponCode
            };
            _paymentService.MakePayment(paymentRequest);

            // Başarılı yanıt döndürme
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            // Hata durumunda hata yanıtı döndürme
            return new ApiResponse(ex.Message);
        }
    }

    private string GenerateOrderNumber()
    {
        // Rastgele bir sayı üretme
        var random = new Random();
        var number = random.Next(100000, 999999).ToString();

        // Sayıyı 9 karaktere tamamlama
        number = number.PadLeft(9, '0');

        return number;
    }
    public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetUserOrders(string userId)
    {
        try
        {
            var userWithOrdersList = await Task.FromResult(_unitOfWork.Repository<Order>().GetAllWithInclude("OrderDetails").Where(x => x.UserId == userId));
            if (userWithOrdersList is null)
            {
                return new ApiResponse<IEnumerable<OrderResponse>>("Record not found");
            }

            var mapped = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponse>>(userWithOrdersList);
            return new ApiResponse<IEnumerable<OrderResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<OrderResponse>>(ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersIsActive()
    {
        try
        {
            var activeOrder = await Task.FromResult(_unitOfWork.Repository<Order>().GetAllWithInclude("OrderDetails").Where(x => x.IsActive == true ));
            if (activeOrder is null)
            {
                return new ApiResponse<IEnumerable<OrderResponse>>("Aktif sipariş yok");
            }
            var mapped = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponse>>(activeOrder);
            return new ApiResponse<IEnumerable<OrderResponse>>(mapped);
        }
        catch (Exception ex)
        {

            return new ApiResponse<IEnumerable<OrderResponse>>(ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersIsPasif()
    {
        try
        {
            var activeOrder = await Task.FromResult(_unitOfWork.Repository<Order>().GetAllWithInclude("OrderDetails").Where(x => x.IsActive == false || x.OrderDate < DateTime.UtcNow));
            if (activeOrder is null)
            {
                return new ApiResponse<IEnumerable<OrderResponse>>("Pasif sipariş yok");
            }
            var mapped = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponse>>(activeOrder);
            return new ApiResponse<IEnumerable<OrderResponse>>(mapped);
        }
        catch (Exception ex)
        {

            return new ApiResponse<IEnumerable<OrderResponse>>(ex.Message);
        }
    }

    public async Task<ApiResponse<IEnumerable<OrderResponse>>> GetOrdersNumber(string orderNumber)
    {
        try
        {
            var orderNumberFind=await Task.FromResult(_unitOfWork.Repository<Order>().GetAllWithInclude("OrderDetails").Where(x=>x.OrderNumber.Equals(orderNumber)));
            if (orderNumber is null)
            {
                return new ApiResponse<IEnumerable<OrderResponse>>("Record not found");
            }
            var mapped=_mapper.Map<IEnumerable<Order>, IEnumerable<OrderResponse>>(orderNumberFind);
            return new ApiResponse<IEnumerable<OrderResponse>>(mapped);

        }
        catch (Exception ex)
        {

            return new ApiResponse<IEnumerable<OrderResponse>>(ex.Message);
        }
    }
}
