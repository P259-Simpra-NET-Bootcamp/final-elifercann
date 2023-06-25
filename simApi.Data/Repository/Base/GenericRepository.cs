using Microsoft.EntityFrameworkCore;
using simApi.Base;
using System.Linq.Expressions;

namespace simApi.Data.Repository;
public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
{
    protected readonly ApplicationContext dbContext;
    private bool disposed;

    public GenericRepository(ApplicationContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void Delete(Entity entity)
    {
        dbContext.Set<Entity>().Remove(entity);
    }

    public void DeleteById(int id)
    {
        var entity = dbContext.Set<Entity>().Find(id);
        dbContext.Set<Entity>().Remove(entity);
    }

    public List<Entity> GetAll()
    {
        return dbContext.Set<Entity>().ToList();
    }
    public List<Entity> GetAllAsNoTracking()
    {
        return dbContext.Set<Entity>().AsNoTracking().ToList();
    }
    public List<Entity> GetAllWithInclude(params string[] includes)
    {
        var query = dbContext.Set<Entity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return query.ToList();
    }

    public Entity GetByIdWithInclude(int id, params string[] includes)
    {
        var query = dbContext.Set<Entity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return query.FirstOrDefault(x => x.Id == id);
    }
    public Entity GetById(int id)
    {
        return dbContext.Set<Entity>().Where(p => p.Id == id).FirstOrDefault();
    }
    public Entity GetByIdAsNoTracking(int id)
    {
        return dbContext.Set<Entity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public void Insert(Entity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedBy = "simshop@sim.com";

        dbContext.Set<Entity>().Add(entity);
    }

    public void Update(Entity entity)
    {
        dbContext.Set<Entity>().Update(entity);
    }

    public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression)
    {
        return dbContext.Set<Entity>().Where(expression).AsQueryable();
    }
    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void CompleteWithTransaction()
    {
        using (var dbDcontextTransaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.SaveChanges();
                dbDcontextTransaction.Commit();
            }
            catch (Exception ex)
            {
                // logging
                dbDcontextTransaction.Rollback();
            }
        }
    }

    private void Clean(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        disposed = true;
        GC.SuppressFinalize(this);
    }
    public void Dispose()
    {
        Clean(true);
    }

    public void DeleteWith(int id)
    {

        var entity = dbContext.Set<Entity>().Find(id);
        if (entity != null)
        {
            dbContext.Set<Entity>().Remove(entity);
            dbContext.SaveChanges();
        }
    }

}