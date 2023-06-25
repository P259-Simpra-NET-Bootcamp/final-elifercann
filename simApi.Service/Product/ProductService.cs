using AutoMapper;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Schema;

namespace simApi.Service;

public class ProductService : BaseService<Product, ProductRequest, ProductResponse>, IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override ApiResponse<List<ProductResponse>> GetAll()
    {
        try
        {
            var entityList = _unitOfWork.Repository<Product>().GetAll();
            var mapped = _mapper.Map<List<Product>, List<ProductResponse>>(entityList);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<ProductResponse>>(ex.Message);
        }
    }
    public override ApiResponse Insert(ProductRequest request)
    {
        try
        {
            var entity = _unitOfWork.Repository<Category>().GetById(request.CategoryId);
            if (entity == null)
            {
                return new ApiResponse("Record not found");
            }
          
            var mapped=_mapper.Map<ProductRequest,Product>(request);
            _unitOfWork.Repository<Product>().Insert(mapped);
            _unitOfWork.Complete();
            return new ApiResponse();
        }
        catch (Exception ex)
        {

            return new ApiResponse(ex.Message);
        }
    }
  
}
