using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using simApi.Data;
using simApi.Data.Context;

namespace simApi.Presentation.RestExtension;

public static class DbContextExtension
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        var dbType = Configuration.GetConnectionString("DbType");
        if (dbType == "Mssql")
        {
            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<ApplicationContext>(opts =>
            opts.UseSqlServer(dbConfig));
        }
        else if (dbType == "PostgreSql")
        {
            var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddDbContext<ApplicationContext>(opts =>
              opts.UseNpgsql(dbConfig));
        }

        services.AddScoped<DapperSimDbContext>();

        services.AddIdentity<AppUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 1;
            options.User.RequireUniqueEmail = true;
        });
       
    }
}