using simApi.Data;
using simApi.Schema;

namespace simApi.Service;

public interface IProductService : IBaseService<Product, ProductRequest, ProductResponse>
{
}
