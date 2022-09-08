using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DataContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Base.DataContext
{
    public class Uow  : IUow
    {
        public DbContext _context { get; }
        private readonly IServiceProvider _serviceProvider;

        public Uow(DbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public void Commit() => _context.SaveChanges();

        public async Task CommitAsync() => await _context.SaveChangesAsync();

        public T Repo<T>() => _serviceProvider.GetRequiredService<T>();

        public async Task CreateAsync<T>(T entity) => await _context.AddAsync(entity);
        public async Task CreateRangeAsync<T>(IEnumerable<T> list) where T : class => await _context.AddRangeAsync(list);

        public void Update<T>(T entity) => _context.Update(entity);

        public void UpdateRange<T>(IEnumerable<T> list) where T : class => _context.UpdateRange(list);

        public void Remove<T>(T entity) => _context.Remove(entity);

        public void RemoveRange<T>(IEnumerable<T> list) where T : class => _context.RemoveRange(list);
    }
}