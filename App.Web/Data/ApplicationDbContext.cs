using App.Base.GenericModel;
using App.Base.Providers.Interface;
using App.Base.GenericModel.Interfaces;
using App.Base.DataContext;
using App.User;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConnectionProvider _connectionProvider;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConnectionProvider connectionProvider) : base(options)
    {
        _connectionProvider = connectionProvider;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // EntitySnakeCaseConverter.ConvertEntityToSnakeCase(builder);
        builder.AddGlobalHasQueryFilterForBaseTypeEntities<GenericModel>(x => x.RecStatus != 'D');
        builder.AddUser();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseNpgsql(_connectionProvider.GetConnection())
            .UseSnakeCaseNamingConvention();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaveChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaveChanges()
    {
        HandleSoftDelete();
        HandleUpdatedDate();
    }

    private void HandleUpdatedDate()
    {
        var updatedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (var updatedEntry in updatedEntries)
        {
            if (updatedEntry.Entity is GenericModel model)
            {
                model.UpdatedDate = DateTime.UtcNow;
            }
        }
    }

    private void HandleSoftDelete()
    {
        var deletedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
        foreach (var deletedEntry in deletedEntries)
        {
            if(deletedEntry.Entity is not ISoftDelete) continue;
            deletedEntry.State = EntityState.Modified;
            if (deletedEntry.Entity is not GenericModel model) continue;
            model.RecStatus = 'D';
            Entry(model).Property(nameof(model.RecStatus)).IsModified = true;
        }
    }
}