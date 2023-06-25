using simApi.Base;
using System.Linq.Expressions;

namespace simApi.Data.Repository;

public interface IGenericRepository<Entity> where Entity : BaseModel
{
    Entity GetById(int id);
    Entity GetByIdAsNoTracking(int id);
    Entity GetByIdWithInclude(int id, params string[] includes);
    void Insert(Entity entity);
    void Update(Entity entity);
    void DeleteById(int id);
    void Delete(Entity entity);
    void DeleteWith(int id);
    List<Entity> GetAll();
    List<Entity> GetAllAsNoTracking();
    List<Entity> GetAllWithInclude(params string[] includes);
    IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression);

    void Complete();
    void CompleteWithTransaction();
}
