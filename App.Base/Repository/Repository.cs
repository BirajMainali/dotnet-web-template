using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Base.Repository;

public class Repository<T, TKey> : IRepository<T, TKey> where T : class
{
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }

    public virtual async Task<T?> FindByAsync(TKey id) => await _dbSet.FindAsync(id);

    public virtual T? Find(TKey id) => _dbSet.Find(id);

    public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
    {
        predicate ??= x => true;
        return await _dbSet.CountAsync(predicate);
    }

    public virtual async Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate) => await _dbSet.AnyAsync(predicate);

    public virtual Task<T?> GetItemAsync(Expression<Func<T, bool>> predicate) => _dbSet.FirstOrDefaultAsync(predicate);

    public virtual Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
    {
        predicate ??= x => true;
        return _dbSet.Where(predicate).ToListAsync();
    }

    public virtual List<T> Get(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).ToList();

    public virtual IQueryable<T?> GetQueryable() => _dbSet.AsQueryable();

    public virtual async Task<T> FindOrThrowAsync(TKey id, string message)
    {
        var entity = await FindByAsync(id);
        if (entity == null)
        {
            throw new Exception(message);
        }

        return entity;
    }
}