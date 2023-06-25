using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using simApi.Data.Repository;

namespace simApi.Controller.ContextFactory;

public class DbSimContexttFactory : IDesignTimeDbContextFactory<DbSimContextt>
{
    public DbSimContextt CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

        // DbContextOptionsBuilder
        var builder = new DbContextOptionsBuilder<DbSimContextt>()
            .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
            prj => prj.MigrationsAssembly("simApi.Controller"));

        return new DbSimContextt(builder.Options);
    }
}
