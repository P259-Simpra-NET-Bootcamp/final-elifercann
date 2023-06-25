using simApi.Base;
using simApi.Data.Repository;

namespace simApi.Data.UnitOfWork;

public interface IUnitOfWork:IDisposable
{
    IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel;
    void Complete();
    void CompleteWithTransaction();
}
