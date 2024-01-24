using System.Reflection;
using System.Security.Claims;
using App.Base.Constants;
using App.Base.Entities;
using App.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace App.Web.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IWebHostEnvironment _env;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor contextAccessor, IWebHostEnvironment env) :
        base(options)
    {
        _contextAccessor = contextAccessor;
        _env = env;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddUser();
        AddSafeDeleteGlobalQuery(builder);
        base.OnModelCreating(builder);
    }


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp with time zone");
    }

    private static readonly ILoggerFactory ConsoleLogger
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSnakeCaseNamingConvention();
        base.OnConfiguring(optionsBuilder);
        if (_env.IsDevelopment())
        {
            optionsBuilder.UseLoggerFactory(ConsoleLogger);
        }
    }

    public void SetGlobalQuery<T>(ModelBuilder builder) where T : FullAuditedEntity<object>
    {
        builder.Entity<T>().HasQueryFilter(e => e.RecStatus.Equals('A'));
    }

    private static readonly MethodInfo SetGlobalQueryMethod = typeof(ApplicationDbContext)
        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == nameof(SetGlobalQuery));

    private void AddSafeDeleteGlobalQuery(ModelBuilder builder)
    {
        foreach (var type in builder.Model.GetEntityTypes())
        {
            if (type.BaseType != null || !typeof(FullAuditedEntity<>).IsAssignableFrom(type.ClrType)) continue;
            var method = SetGlobalQueryMethod.MakeGenericMethod(type.ClrType);
            method.Invoke(this, new object[] { builder });
        }
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            AddAuditLog(entry);
        }

        return base.SaveChanges();
    }

    private void AddAuditLog(EntityEntry entry)
    {
        if (entry.Entity is FullAuditedEntity<object> entity)
        {
            if (entry.State == EntityState.Deleted)
            {
                entity.RecStatus = Status.Active;
                entry.State = EntityState.Modified;
            }

            if (entry.State == EntityState.Added)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy = _contextAccessor.HttpContext?.User.FindFirstValue("Id") ?? "~";
                entry.State = EntityState.Modified;
            }

            if (entry.State == EntityState.Modified)
            {
                entity.UpdatedDate = DateTime.UtcNow;
                entity.UpdatedBy = _contextAccessor.HttpContext?.User.FindFirstValue("Id") ?? "~";
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            AddAuditLog(entry);
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}