﻿using System.Linq.Expressions;

namespace HopsHub.Api.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByLongIdAsync(long id);
    IQueryable<T> GetQuerable();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveAsync();
    Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync();
}