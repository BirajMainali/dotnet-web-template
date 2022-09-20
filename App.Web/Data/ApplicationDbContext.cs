using App.Base.GenericModel;
using App.Base.GenericModel.Interfaces;
using App.Base.DataContext;
using App.User;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        EntitySnakeCaseConverter.ConvertEntityToSnakeCase(builder);
        builder.AddGlobalHasQueryFilterForBaseTypeEntities<GenericModel>(x => x.RecStatus != 'D');
        builder.AddUser();
    }

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