using System.Transactions;
using App.Base.DataContext.Interfaces;
using App.Base.Extensions;
using App.Base.Settings;
using App.User.Dto;
using App.User.Entity;
using App.User.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace App.User.Handler;

public class MultiTenantHandler : IMultiTenantHandler
{
    private readonly IUow _uow;
    private readonly IUserService _userService;
    private readonly IOptions<AppSettings> _options;

    private readonly DbContext _context;

    public MultiTenantHandler(IUow uow, IUserService userService,
        IOptions<AppSettings> options, DbContext context)
    {
        _uow = uow;
        _userService = userService;
        _options = options;
        _context = context;
    }

    public async Task HandleAsync(UserDto dto, string tenantName)
    {
        tenantName = tenantName.IgnoreCase();
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        await ResolveUser(dto, tenantName);
        tsc.Complete();

        if (_options.Value.UseMultiTenancy)
        {
            _context.Database.SetConnectionString(tenantName.ToSnakeCase());
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await ResolveUser(dto, tenantName);
            scope.Complete();
        }
    }

    private async Task<AppUser> ResolveUser(UserDto dto, string tenantName)
    {
        if (_options.Value.UseMultiTenancy)
        {
            var user = await _userService.CreateUser(dto);
            return user;
        }
        else
        {
            tenantName = tenantName.IgnoreCase();
            var appUser = await _userService.CreateUser(dto);
            var applicationTenant = new ApplicationTenant(tenantName, tenantName.ToSnakeCase());
            await _uow.CreateAsync(applicationTenant);
            appUser.SetTenant(applicationTenant);
            _uow.Update(appUser);
            await _uow.CommitAsync();
            return appUser;
        }
    }
}