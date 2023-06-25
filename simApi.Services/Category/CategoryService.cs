using AutoMapper;
using simApi.Data;
using simApi.Data.Repository;
using simApi.Schema;

namespace simApi.Services;

public class CategoryService : ICategoryService
{

    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;

    public CategoryService(IRepositoryManager manager,IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<CategoryRequest> CreateOneCategory(CategoryRequest request)
    {

        var entity = _mapper.Map<CategoryRequest, Category>(request);
        _manager.Category.CreateOneCategory(entity);
        await _manager.SaveAsync();
        return _mapper.Map<CategoryRequest>(entity);

    }

    public async Task DeleteOneCategory(int id, bool trackChanges)
    {
        var entity = await GetOneCategoryByIdAndCheckExists(id, trackChanges);
        var mapped = _mapper.Map<CategoryResponse, Category>(entity);
        _manager.Category.DeleteOneCategory(mapped);
        await _manager.SaveAsync();
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync(bool trackChanges)
    {
        var list = await _manager.Category.GetAllCategoriesAsync(trackChanges);
        var mapped=_mapper.Map<IEnumerable<Category>,IEnumerable<CategoryResponse >> (list);
        return mapped;
    }

    public async Task<CategoryResponse> GetOneCategoryByIdAsync(int id, bool trackChanges)
    {
        var entity=await _manager.Category.GetByIdAsync(id, trackChanges);
        if(entity is null)
            //mesaj ekle en son apiresponse çekebilirsin
            return new CategoryResponse();
        var mapped=_mapper.Map<Category,CategoryResponse> (entity);
        return mapped;
    }

    public async Task UpdateOneCategory(int id, bool trackChanges, CategoryRequest request)
    {
        var entity = await GetOneCategoryByIdAndCheckExistsRequest(id, trackChanges);
        var mapped = _mapper.Map<Category>(entity);
        _manager.Category.UpdateOneCategory(mapped);
        await _manager.SaveAsync(); 
    }
    private async Task<CategoryResponse> GetOneCategoryByIdAndCheckExists(int id, bool trackChanges)
    {
        // check entity 
        var entity = await _manager.Category.GetByIdAsync(id, trackChanges);

        if (entity is null)
            return new CategoryResponse();
        var mapped = _mapper.Map<Category, CategoryResponse>(entity);

        return mapped;
    }
    private async Task<CategoryRequest> GetOneCategoryByIdAndCheckExistsRequest(int id, bool trackChanges)
    {
        // check entity 
        var entity = await _manager.Category.GetByIdAsync(id, trackChanges);

        if (entity is null)
            return new CategoryRequest();
        var mapped = _mapper.Map<Category,CategoryRequest>(entity);

        return mapped;
    }
}
