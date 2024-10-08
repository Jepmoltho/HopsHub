using HopsHub.Api.Services.Interfaces;
using HopsHub.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HopsHub.Api.Services;


public class Repository<T> : IRepository<T> where T : class
{
	private readonly BeerContext _context;

	private readonly DbSet<T> _dbSet;

	public Repository(BeerContext context)
	{
		_context = context;
		_dbSet = _context.Set<T>();
	}

    public async Task<List<T>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

    public IQueryable<T> GetQuerable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }
}