#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Base.Repository;

public interface IRepository<T, in TKey> where T : class
{
    Task<T?> FindByAsync(TKey id);

    T? Find(TKey id);

    Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);

    Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate);

    Task<T?> GetItemAsync(Expression<Func<T, bool>> predicate);

    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

    List<T> Get(Expression<Func<T, bool>> predicate);

    IQueryable<T?> GetQueryable();

    Task<T> FindOrThrowAsync(TKey id);
}