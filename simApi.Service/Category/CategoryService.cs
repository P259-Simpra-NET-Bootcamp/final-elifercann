using AutoMapper;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Schema;

namespace simApi.Service;

public class CategoryService : BaseService<Category, CategoryRequest, CategoryResponse>, ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork, mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override ApiResponse<List<CategoryResponse>> GetAll()
    {
        var list= _unitOfWork.Repository<Category>().GetAllWithInclude("Products");
        var mapped=_mapper.Map<List<Category>,List<CategoryResponse>>(list);
        return new ApiResponse<List<CategoryResponse>>(mapped);
           
    }

    public override ApiResponse<CategoryResponse> GetById(int id)
    {
        var list = _unitOfWork.Repository<Category>().GetByIdWithInclude(id,"Products");
        if (list is null)
        {
            return new ApiResponse<CategoryResponse>("Category not found");
        }
        var mapped = _mapper.Map<Category,CategoryResponse>(list);
        return new ApiResponse<CategoryResponse>(mapped);

    }
    public override ApiResponse Delete(int Id)
    {
        var category = _unitOfWork.Repository<Category>().GetByIdWithInclude(Id, "Products");

        if (category.Products.Any())
        {
            return new ApiResponse ("Category has products. Deletion not allowed." );
        }

        _unitOfWork.Repository<Category>().DeleteWith(Id);
        return new ApiResponse ("Deletion successful" );
    }

}
   

  

