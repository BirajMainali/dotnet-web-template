using System.Transactions;
using App.Base.DataContext.Interfaces;
using App.Base.Extensions;
using App.Base.Providers.Interface;
using App.Base.Settings;
using App.User.Dto;
using App.User.Entity;
using App.User.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace App.User.Handler;

public class MultiTenantHandler : IMultiTenantHandler
{
    private readonly IUow _uow;
    private readonly IUserService _userService;
    private readonly IOptions<AppSettings> _options;

    private readonly DbContext _context;
    private readonly ILogger<IMultiTenantHandler> _logger;
    private readonly IDatabaseConnectionProvider _databaseConnectionProvider;

    public MultiTenantHandler(IUow uow, IUserService userService,
        IOptions<AppSettings> options, DbContext context, ILogger<IMultiTenantHandler> logger,
        IDatabaseConnectionProvider databaseConnectionProvider)
    {
        _uow = uow;
        _userService = userService;
        _options = options;
        _context = context;
        _logger = logger;
        _databaseConnectionProvider = databaseConnectionProvider;
    }

    public async Task<AppUser> HandleAsync(UserDto dto, string tenantName)
    {
        tenantName = tenantName.IgnoreCase();
        _logger.LogInformation("Request to create user {@dto} for tenant {tenantName}", dto, tenantName);
        var (user, tenant) = await ResolveUser(dto, tenantName);
        await ConfigureDatabaseIfRequired(tenant: tenant, user);
        _logger.LogInformation("User created {@user}", user);
        return user;
    }

    private async Task ConfigureDatabaseIfRequired(ApplicationTenant? tenant, AppUser user)
    {
        if (_options.Value.UseMultiTenancy)
        {
            _logger.LogInformation("Request to create user {@dto} for tenant {tenantName}", user, tenant!.Name);
            var connectionString = _databaseConnectionProvider.GetConnectionString(tenant.Name.ToSnakeCase());
            _context.Database.SetConnectionString(connectionString);
            _logger.LogInformation("Connection string set to {connectionString}", connectionString);
            await _context.Database.MigrateAsync();
            _logger.LogInformation("Database migrated");
            _logger.LogInformation("Connection string set to {connectionString}", tenant.Name.ToSnakeCase());
            await _uow.CreateAsync(user);
            await _uow.CreateAsync(tenant);
            user.SetTenant(tenant);
            await _uow.CommitAsync();
        }
    }

    private async Task<(AppUser user, ApplicationTenant? applicationTenant)> ResolveUser(UserDto dto, string tenantName)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var user = await _userService.CreateUser(dto);
        ApplicationTenant applicationTenant = null;
        if (_options.Value.UseMultiTenancy)
        {
            applicationTenant = new ApplicationTenant(tenantName, tenantName.ToSnakeCase());
            await _uow.CreateAsync(applicationTenant);
            user.SetTenant(applicationTenant);
        }

        await _uow.CommitAsync();
        tsc.Complete();
        return (user, applicationTenant);
    }
}