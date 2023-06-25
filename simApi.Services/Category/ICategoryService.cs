using simApi.Schema;

namespace simApi.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync(bool trackChanges);
    Task<CategoryResponse> GetOneCategoryByIdAsync(int id, bool trackChanges);
    Task<CategoryRequest> CreateOneCategory(CategoryRequest request);
    Task UpdateOneCategory(int id, bool trackChanges, CategoryRequest request);
    Task DeleteOneCategory(int id, bool trackChanges);
}