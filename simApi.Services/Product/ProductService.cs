using AutoMapper;
using simApi.Data;
using simApi.Data.Repository;
using simApi.Schema;

namespace simApi.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _manager;
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;

    public ProductService(IRepositoryManager manager, IMapper mapper, ICategoryService categoryService)
    {
        _manager = manager;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    public async Task<ProductRequest> CreateOneProduct(ProductRequest request)
    {
        var category = await _categoryService.GetOneCategoryByIdAsync(request.CategoryId, false);
        var entity=_mapper.Map<ProductRequest,Product>(request);
        _manager.Product.CreateOneProduct(entity);
        await _manager.SaveAsync();
        return _mapper.Map<ProductRequest>(entity);
    }

    public async Task DeleteOneProduct(int id,bool trackChanges)
    {
        var entity=await GetOneProductByIdAndCheckExists(id,trackChanges);
        var mapped=_mapper.Map<ProductResponse,Product>(entity);
        _manager.Product.DeleteOneProduct(mapped);
        await _manager.SaveAsync();
    }

    public async Task<List<ProductResponse>> GetAllProductAsync(bool trackChanges)
    {
        var products = await _manager.Product.GetAllProductsAsync(trackChanges);
        var mapped=_mapper.Map<List<Product>,List<ProductResponse>>(products);
        return mapped;
    }

    public async Task<ProductResponse> GetOneProductByIdAsync(int id, bool trackChanges)
    {
        var product=await GetOneProductByIdAndCheckExists(id, trackChanges);    
        return product;
    }

    public async Task UpdateOneProduct(int id, bool trackChanges, ProductRequest request)
    {
        
        var entity = await GetOneProductByIdAndCheckExistsRequest(id, trackChanges);
        var mapped = _mapper.Map<Product>(entity);
        _manager.Product.UpdateOneProduct(mapped);
        await _manager.SaveAsync();

    }

    private async Task<ProductResponse> GetOneProductByIdAndCheckExists(int id, bool trackChanges)
    {
        // check entity 
        var entity = await _manager.Product.GetOneProductByIdAsync(id, trackChanges);

        if (entity is null)
            return new ProductResponse();
        var mapped=_mapper.Map<Product,ProductResponse>(entity);


        return mapped;
    }
    private async Task<ProductRequest> GetOneProductByIdAndCheckExistsRequest(int id, bool trackChanges)
    {
        // check entity 
        var entity = await _manager.Product.GetOneProductByIdAsync(id, trackChanges);

        if (entity is null)
            return new ProductRequest();
        var mapped = _mapper.Map<Product, ProductRequest>(entity);



        return mapped;
    }

}
