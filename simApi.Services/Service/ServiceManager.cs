namespace simApi.Services;

public class ServiceManager : IServiceManager
{
    private readonly ICategoryService _categoryService;
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IAuthenticationService _authService;

    public ServiceManager(ICategoryService categoryService, IProductService productService, 
        IOrderService orderService, IAuthenticationService authService)
    {
        _categoryService = categoryService;
        _productService = productService;
        _orderService = orderService;
        _authService = authService;
    }

    public ICategoryService CategoryService => _categoryService;

    public IProductService ProductService => _productService;

    public IOrderService OrderService =>_orderService;

    public IAuthenticationService AuthenticationService => _authService;
}
