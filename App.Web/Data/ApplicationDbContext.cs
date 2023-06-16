using System.Reflection;
using System.Security.Claims;
using App.Base.GenericModel;
using App.Base.GenericModel.Interfaces;
using App.User;
using Microsoft.EntityFrameworkCore;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace App.Web.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IWebHostEnvironment _env;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor contextAccessor, IWebHostEnvironment env) :
        base(options)
    {
        _contextAccessor = contextAccessor;
        _env = env;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddUser();
        AddSafeDeleteGlobalQuery(builder);
        //builder.ConfigureUserTableName();
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

    public void SetGlobalQuery<T>(ModelBuilder builder) where T : GenericModel
    {
        builder.Entity<T>().HasQueryFilter(e => e.RecStatus.Equals('A'));
    }

    private static readonly MethodInfo SetGlobalQueryMethod = typeof(ApplicationDbContext)
        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

    private void AddSafeDeleteGlobalQuery(ModelBuilder builder)
    {
        foreach (var type in builder.Model.GetEntityTypes())
        {
            if (type.BaseType != null || !typeof(ISoftDelete).IsAssignableFrom(type.ClrType)) continue;
            var method = SetGlobalQueryMethod.MakeGenericMethod(type.ClrType);
            method.Invoke(this, new object[] { builder });
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            if (entry.State != EntityState.Deleted || entity is not ISoftDelete) continue;
            entry.State = EntityState.Modified;
            entity.GetType().GetProperty("RecStatus")?.SetValue(entity, 'D');
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            if (entry.State != EntityState.Deleted || entity is not ISoftDelete) continue;
            entry.State = EntityState.Modified;
            entry.GetType().GetProperty("RecStatus")?.SetValue(entity, 'D');
            if (entry.State == EntityState.Added)
            {
                var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                entry.GetType().GetProperty("RecById")?.SetValue(entity, userId);
            }

            if (entry.State == EntityState.Modified)
            {
                entry.GetType().GetProperty("UpdatedDate")?.SetValue(entity, DateTime.UtcNow);
            }
        }

        return base.SaveChanges();
    }
}