using System.Transactions;
using App.Base.DataContext.Interfaces;
using App.Base.Extensions;
using App.Base.Providers.Interface;
using App.Base.Settings;
using App.User.Dto;
using App.User.Entity;
using App.User.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        var user = await ResolveUser(dto, tenantName);
        await ConfigureDatabaseIfRequired(dto, tenantName);
        _logger.LogInformation("User created {@user}", user);
        return user;
    }

    private async Task ConfigureDatabaseIfRequired(UserDto dto, string tenantName)
    {
        if (_options.Value.UseMultiTenancy)
        {
            _logger.LogInformation("Request to create user {@dto} for tenant {tenantName}", dto, tenantName);
            var connectionString = _databaseConnectionProvider.GetConnectionString(tenantName.ToSnakeCase());
            _context.Database.SetConnectionString(connectionString);
            _logger.LogInformation("Connection string set to {connectionString}", connectionString);
            await _context.Database.MigrateAsync();
            _logger.LogInformation("Database migrated");
            _logger.LogInformation("Connection string set to {connectionString}", tenantName.ToSnakeCase());
            await ResolveUser(dto, tenantName);
        }
    }

    private async Task<AppUser> ResolveUser(UserDto dto, string tenantName)
    {
        using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var user = await _userService.CreateUser(dto);
        if (_options.Value.UseMultiTenancy)
        {
            var applicationTenant = new ApplicationTenant(tenantName, tenantName.ToSnakeCase());
            await _uow.CreateAsync(applicationTenant);
            user.SetTenant(applicationTenant);
            return user;
        }

        await _uow.CommitAsync();
        tsc.Complete();
        return user;
    }
}