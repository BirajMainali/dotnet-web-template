using App.Base.Configurations;
using App.User.Dto;
using App.User.Entity;

namespace App.User.Handler;

public interface IMultiTenantHandler : IScopedDependency
{
    Task<AppUser> HandleAsync(UserDto dto, string tenantName);
}