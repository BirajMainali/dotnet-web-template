using Base.GenericModel;
using Microsoft.EntityFrameworkCore;
using VehicleManagement.User.Application;

namespace VehicleManagement.Web.Data;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
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
    }
    private void HandleSoftDelete()
    {
        var deletedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
        foreach (var deletedEntry in deletedEntries)
        {
            deletedEntry.State = EntityState.Modified;
            if (deletedEntry.Entity is not GenericModel model) continue;
            model.RecStatus = 'D';
            Entry(model).Property(nameof(model.RecStatus)).IsModified = true;
        }
    }
}