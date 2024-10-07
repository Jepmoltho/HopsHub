using System.Linq.Expressions;

namespace HopsHub.Api.Services.Interfaces;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    IQueryable<T> GetQuerable();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveAsync();
    Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
}