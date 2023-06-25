using Microsoft.AspNetCore.Identity;
using Serilog;
using simApi.Base;
using simApi.Data;
using simApi.Data.UnitOfWork;
using simApi.Presentation.Middleware;
using simApi.Presentation.RestExtension;

namespace simApi.Presentation;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public static JwtConfig JwtConfig { get; private set; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
       
        JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddCors(options => options.AddPolicy("Cors", builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));
        services.AddCustomSwaggerExtension();
        services.AddDbContextExtension(Configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMapperExtension();
        services.AddServiceExtension();
        services.AddJwtExtension();
        services.AddHttpContextAccessor();
        services.AddScoped<TokenAuthenticationMiddleware>();


    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DefaultModelsExpandDepth(-1);
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sim Company");
            c.DocumentTitle = "SimApi Company";
        });

        app.UseMiddleware<TokenAuthenticationMiddleware>();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
        {
            Log.Information("-------------Request-Begin------------");
            Log.Information(requestProfilerModel.Request);
            Log.Information(Environment.NewLine);
            Log.Information(requestProfilerModel.Response);
            Log.Information("-------------Request-End------------");
        };
        app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);
      
        app.UseHttpsRedirection();

        app.UseCors("Cors");
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}