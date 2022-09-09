﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Base.ValueObject;

namespace App.Base.GenericRepository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        List<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetItemAsync(Expression<Func<T, bool>> predicate);
        Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindAsync(long id);
        Task<bool> CheckIfExistAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindOrThrowAsync(long id);
        IQueryable<T> GetQueryable();
        PagedResult<T> Paginate(IQueryable<T> queryable, int page = 1, int limit = 100);
    }
}