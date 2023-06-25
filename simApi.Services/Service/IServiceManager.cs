namespace simApi.Services;

public interface IServiceManager
{
    ICategoryService CategoryService { get; }
    IProductService ProductService { get; }
    IOrderService OrderService { get; }
    IAuthenticationService AuthenticationService { get; }
}
