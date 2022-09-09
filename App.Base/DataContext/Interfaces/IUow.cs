using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Base.DataContext.Interfaces
{
    public interface IUow
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