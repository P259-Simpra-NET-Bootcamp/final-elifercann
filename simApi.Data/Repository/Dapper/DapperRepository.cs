using Dapper;
using simApi.Base;
using simApi.Data.Context;
using static Dapper.SqlMapper;

namespace simApi.Data.Repository;

public class DapperRepository<Entity> : IDapperRepository<Entity> where Entity : BaseModel
{
    protected readonly DapperSimDbContext _dapperContext;
    private bool disposed;

    public DapperRepository(DapperSimDbContext dapperContext)
    {
        _dapperContext= dapperContext;
    }
    public void DeleteById(int id)
    {
        string sql = $"DELETE FROM {GetTableName()} WHERE Id = @Id";
        _dapperContext.CreateConnection().Execute(sql, new { Id = id });
    }

  

    public List<Entity>  GetAll()
    {
        string sql = $"SELECT * FROM {GetTableName()}";
        return  _dapperContext.CreateConnection().Query<Entity>(sql).ToList();
    }

    public Entity GetById(int id)
    {
        string sql = $"SELECT * FROM {GetTableName()} WHERE Id = @Id";
       
         return  _dapperContext.CreateConnection().QuerySingleOrDefault<Entity>(sql, new { Id = id });
    }

    public void Insert(Entity entity)
    {
        string sql = $"INSERT INTO {GetTableName()} (Id, Column1, Column2, ...) VALUES (@Id, @Column1, @Column2, ...)";
        _dapperContext.CreateConnection().Execute(sql, entity);
    }

    public void Update(Entity entity)
    {
        var entityType = typeof(Entity);
        var properties = entityType.GetProperties();

        var updateColumns = properties.Select(p => $"{p.Name} = @{p.Name}").ToList();
        string sql = $"UPDATE {GetTableName()} SET {string.Join(", ", updateColumns)} WHERE Id = @Id";

        _dapperContext.CreateConnection().Execute(sql, entity);
    }

    private string GetTableName()
    {
        var entityType = typeof(Entity);
        var attribute = entityType.GetCustomAttributes(false).SingleOrDefault(a => a.GetType().Name == "TableAttribute");
        var tableName = attribute?.GetType().GetProperty("Name")?.GetValue(attribute, null) as string;
        return tableName ?? entityType.Name;
    }
}