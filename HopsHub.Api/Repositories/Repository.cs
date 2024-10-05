using HopsHub.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HopsHub.Api.Repositories;


public class Repository<T> : IRepository<T> where T : class
{
	private readonly DbContext _DbContext;

	private readonly DbSet<T> _DbSet;

	public Repository(DbContext dbContext)
	{
		_DbContext = dbContext;
		_DbSet = _DbContext.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		var result = await _DbSet.ToListAsync();

		return result;
	}

}

