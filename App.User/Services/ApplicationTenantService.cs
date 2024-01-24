using App.Base.DataContext.Interfaces;
using App.Base.Extensions;
using App.Base.Repository;
using App.User.Entity;
using App.User.Services.Interfaces;

namespace App.User.Services;

public class ApplicationTenantService : IApplicationTenantService
{
    private readonly IRepository<ApplicationTenant, Guid> _applicationTenantRepo;
    private readonly IUow _uow;

    public ApplicationTenantService(IRepository<ApplicationTenant, Guid> applicationTenantRepo, IUow uow)
    {
        _applicationTenantRepo = applicationTenantRepo;
        _uow = uow;
    }

    public async Task<ApplicationTenant> CreateAsync(string name)
    {
        name = name.IgnoreCase();
        var tenant = new ApplicationTenant(name, name.ToSnakeCase());
        await _uow.CreateAsync(tenant);
        await _uow.CommitAsync();
        return tenant;
    }
}