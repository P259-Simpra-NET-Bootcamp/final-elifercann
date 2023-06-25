using simApi.Base;
using simApi.Conrtoller.RestExtension;
using simApi.Controller.RestExtension;

namespace simApi.Controller;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCustomSwaggerExtension();
        services.ConfigureCors();
        services.ConfigureSqlContext(Configuration);
        services.ConfigureRepositoryManager();
        services.ConfigureServiceManager();
        services.ConfigureLoggerService();
        services.ConfigureActionFilters();
        services.ConfigureCors();
        services.ConfigureIdentity();
        services.ConfigureJWT(Configuration);
        services.RegisterRepositories();
        services.RegisterServices();
        services.AddAutoMapper(typeof(Startup));

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
        //app.UseHangfireDashboard();



        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
        // add auth 
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


