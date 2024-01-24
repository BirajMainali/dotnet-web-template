using System.Collections.Generic;
using System.Threading.Tasks;
using App.Base.Configurations;
using Microsoft.EntityFrameworkCore;

namespace App.Base.DataContext.Interfaces
{
    public interface IUow : IScopedDependency
    {
        DbContext Context { get; }
        void Commit();
        Task CommitAsync();
        Task CreateAsync<T>(T entity);
        Task CreateRangeAsync<T>(IEnumerable<T> list) where T : class;
        void Update<T>(T entity);
        void UpdateRange<T>(IEnumerable<T> list) where T : class;
        void Remove<T>(T entity);
        void RemoveRange<T>(IEnumerable<T> list) where T : class;
    }
}