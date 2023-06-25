﻿using simApi.Base;
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
    //IQueryable<Entity> GetAsQueryable();
    List<Entity> GetAllAsNoTracking();
    List<Entity> GetAllWithInclude(params string[] includes);
    IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression);
    //IEnumerable<Entity> WhereAsNoTracking(Expression<Func<Entity, bool>> expression);
    //IEnumerable<Entity> WhereWithInclude(Expression<Func<Entity, bool>> expression, params string[] includes);


    void Complete();
    void CompleteWithTransaction();
}
