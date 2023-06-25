using simApi.Data;
using simApi.Schema;

namespace simApi.Services;

public interface IProductService
{
    Task<List<ProductResponse>> GetAllProductAsync(bool trackChanges);
    Task<ProductResponse> GetOneProductByIdAsync(int id, bool trackChanges);
    Task<ProductRequest> CreateOneProduct(ProductRequest request);
    Task UpdateOneProduct(int id, bool trackChanges,ProductRequest request);
    Task DeleteOneProduct(int id,bool trackChanges);
}
