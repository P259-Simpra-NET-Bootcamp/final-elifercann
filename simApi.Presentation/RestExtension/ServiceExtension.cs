using simApi.Presentation.ActionFilters;
using simApi.Service;

namespace simApi.Presentation.RestExtension;

public static class ServiceExtension
{
    public static void AddServiceExtension(this IServiceCollection services)
    {
        services.AddScoped<IUserLogService, UserLogService>();
        services.AddScoped<ICategoryService,CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderDetailService, OrderDetailService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<ValidationFilterAttribute>();

    }
    
}
