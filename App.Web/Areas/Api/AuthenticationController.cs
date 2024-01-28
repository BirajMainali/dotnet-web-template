using App.Base.Extensions;
using App.Base.Repository;
using App.User.Dto;
using App.User.Entity;
using App.User.Handler;
using App.Web.Manager.Interfaces;
using App.Web.Providers.Interfaces;
using App.Web.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace App.Web.Areas.Api;

[ApiController]
[Area("Api")]
[Route("[area]/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthenticationController : ControllerBase
{
    private readonly IMultiTenantHandler _multiTenantHandler;
    private readonly IRepository<AppUser, long> _userRepo;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IAuthenticator _authenticator;

    public AuthenticationController(
        IMultiTenantHandler multiTenantHandler,
        IRepository<AppUser, long> userRepo,
        ICurrentUserProvider currentUserProvider,
        IAuthenticator authenticator)
    {
        _multiTenantHandler = multiTenantHandler;
        _userRepo = userRepo;
        _currentUserProvider = currentUserProvider;
        _authenticator = authenticator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var result = await _userRepo.GetAllAsync();
            return this.SendSuccess("Success", result);
        }
        catch (Exception e)
        {
            return this.SendError(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] UserVm vm)
    {
        try
        {
            var userDto = new UserDto(vm.Name, vm.Gender, vm.Email, vm.Password, vm.Address, vm.Phone);
            Log.Information("Create user initiated => {@userDto}", userDto);
            var user = await _multiTenantHandler.HandleAsync(userDto, userDto.Email);
            return this.SendSuccess("Success", user);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while creating user");
            return this.SendError(e.Message);
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public async Task<IActionResult> Login([FromForm] LoginVm vm)
    {
        try
        {
            var result = await _authenticator.AuthenticateThoughToken(vm.Email, vm.Password);
            return this.SendSuccess("Success", result);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while logging in");
            return this.SendError(e.Message);
        }
    }

    [HttpGet]
    [Route("WhatIsMyTenant")]
    public IActionResult WhatIsMyTenant()
    {
        try
        {
            var connectionKey = _currentUserProvider.GetCurrentConnectionKey();
            return this.SendSuccess("Success", connectionKey);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while getting tenant");
            return this.SendError(e.Message);
        }
    }
}