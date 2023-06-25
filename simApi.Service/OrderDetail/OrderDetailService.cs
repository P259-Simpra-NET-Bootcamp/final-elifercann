using AutoMapper;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Schema;

namespace simApi.Service;

public class OrderDetailService:BaseService<OrderDetail,OrderDetailRequest,OrderDetailResponse>, IOrderDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper):base (unitOfWork,mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override ApiResponse<List<OrderDetailResponse>> GetAll()
    {
        try
        {
            var orderDetails = _unitOfWork.Repository<OrderDetail>().GetAll();
            var mapped = _mapper.Map<List<OrderDetail>, List<OrderDetailResponse>>(orderDetails);
            return new ApiResponse<List<OrderDetailResponse>>(mapped);
        }
        catch (Exception ex)
        {

            return new ApiResponse<List<OrderDetailResponse>>(ex.Message);
        }
    }

    public override ApiResponse<OrderDetailResponse> GetById(int id)
    {
        try
        {
            var orderWithDetails = _unitOfWork.Repository<OrderDetail>().GetById(id);
            var mapped = _mapper.Map<OrderDetail, OrderDetailResponse>(orderWithDetails);
            return new ApiResponse<OrderDetailResponse>(mapped);

        }
        catch (Exception ex)
        {

            return new ApiResponse<OrderDetailResponse>(ex.Message);
        }
    }
}
