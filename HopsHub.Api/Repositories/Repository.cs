using HopsHub.Api.Interfaces;
using HopsHub.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace HopsHub.Api.Repositories;


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



    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }


}

