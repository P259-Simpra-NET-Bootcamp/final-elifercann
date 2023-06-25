using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using simApi.Controller.ActionFilter;
using simApi.Data;
using simApi.Data.Repository;
using simApi.Services;
using System.Text;

namespace simApi.Conrtoller.RestExtension;

public static class ServiceExtension
{
    public static void AddServiceExtension(this IServiceCollection services)
    {
        //services.AddScoped<IUserLogService, UserLogService>();
        //services.AddScoped<ITokenService, TokenService>();
        //services.AddScoped<IUserService, UserService>();
        //services.AddScoped<ICategoryService, CategoryService>();
        //services.AddScoped<IProductService, ProductService>();
        //services.AddScoped<IOrderService, OrderService>();
        //services.AddScoped<IOrderDetailService, OrderDetailService>();
        

       
    }
   
    public static void ConfigureSqlContext(this IServiceCollection services,
         IConfiguration configuration) => services.AddDbContext<DbSimContextt>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddScoped<ILoggerService, LoggerManager>();


    public static void ConfigureActionFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        services.AddScoped<LogFilterAttribute>();
    }
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
        });
    }
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<User2, IdentityRole>(opts =>
        {
            opts.Password.RequireDigit = true;
            opts.Password.RequireLowercase = false;
            opts.Password.RequireUppercase = false;
            opts.Password.RequireNonAlphanumeric = false;
            opts.Password.RequiredLength = 6;

            opts.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<DbSimContextt>()
            .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["secretKey"];

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            }
        );
    }
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<Services.IProductService, Services.ProductService>();
        services.AddScoped<Services.ICategoryService, Services.CategoryService>();
        services.AddScoped<Services.IAuthenticationService, Services.AuthenticationService>();
        services.AddScoped<Services.IOrderService, Services.OrderService>();
        services.AddScoped<Services.ILoggerService, Services.LoggerManager>();
    }
}
