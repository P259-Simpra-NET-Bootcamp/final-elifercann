using Microsoft.EntityFrameworkCore;
using simApi.Data.Context;

namespace simApi.Controller.RestExtension;

public static class DbContextExtension
{
    public static void AddDbContextExtension(this IServiceCollection services, IConfiguration Configuration)
    {
        var dbType = Configuration.GetConnectionString("DbType");
        if (dbType == "Mssql")
        {
            var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<EfSimDbContext>(opts =>
            opts.UseSqlServer(dbConfig));
        }
        else if (dbType == "PostgreSql")
        {
            var dbConfig = Configuration.GetConnectionString("PostgreSqlConnection");
            services.AddDbContext<EfSimDbContext>(opts =>
              opts.UseNpgsql(dbConfig));
        }

        services.AddScoped<DapperSimDbContext>();
    }
}