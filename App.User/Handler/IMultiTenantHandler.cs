using App.Base.Configurations;
using App.User.Dto;

namespace App.User.Handler;

public interface IMultiTenantHandler : IScopedDependency
{
    Task HandleAsync(UserDto dto, string tenantName);
}