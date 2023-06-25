using simApi.Base;
using simApi.Data.Context;
using simApi.Data.Repository;

namespace simApi.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _efDbContext;
    private readonly DapperSimDbContext _dapperDbContext;
    private bool disposed;
    public UnitOfWork(ApplicationContext efDbContext, DapperSimDbContext dapperDbContext)
    {
        _efDbContext = efDbContext;
        _dapperDbContext = dapperDbContext;
    }
    public void Complete()
    {
        _efDbContext.SaveChanges();
    }
    public void CompleteWithTransaction()
    {
        using (var dbDcontextTransaction = _efDbContext.Database.BeginTransaction())
        {
            try
            {
                _efDbContext.SaveChanges();
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
            if (disposing && _efDbContext is not null)
            {
                _efDbContext.Dispose();
            }
        }

        disposed = true;
        GC.SuppressFinalize(this);
    }
    public void Dispose()
    {
        Clean(true);
    }

    public IDapperRepository<Entity> DapperRepository<Entity>() where Entity : BaseModel
    {
        return new DapperRepository<Entity>(_dapperDbContext);
    }
    public IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel
    {
        return new GenericRepository<Entity>(_efDbContext);
    }
}
