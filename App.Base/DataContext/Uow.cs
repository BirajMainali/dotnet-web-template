using System.Collections.Generic;
using System.Threading.Tasks;
using App.Base.DataContext.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Base.DataContext
{
    public class Uow : IUow
    {
        public DbContext Context { get; }

        public Uow(DbContext context)
        {
            Context = context;
        }

        public void Commit() => Context.SaveChanges();

        public async Task CommitAsync() => await Context.SaveChangesAsync();

        public async Task CreateAsync<T>(T entity) => await Context.AddAsync(entity);

        public async Task CreateRangeAsync<T>(IEnumerable<T> list) where T : class => await Context.AddRangeAsync(list);

        public void Update<T>(T entity) => Context.Update(entity);

        public void UpdateRange<T>(IEnumerable<T> list) where T : class => Context.UpdateRange(list);

        public void Remove<T>(T entity) => Context.Remove(entity);

        public void RemoveRange<T>(IEnumerable<T> list) where T : class => Context.RemoveRange(list);
    }
}