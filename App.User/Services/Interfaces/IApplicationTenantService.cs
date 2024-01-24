using App.Base.Configurations;
using App.User.Entity;

namespace App.User.Services.Interfaces;

public interface IApplicationTenantService : ITransientDependency
{
    Task<ApplicationTenant> CreateAsync(string name);
}