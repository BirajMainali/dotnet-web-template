using App.Base.Extensions;
using App.Base.Repository;
using App.User.Dto;
using App.User.Entity;
using App.User.Handler;
using App.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace App.Web.Areas.Api;

[ApiController]
[Area("Api")]
[Route("[area]/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMultiTenantHandler _multiTenantHandler;
    private readonly IRepository<AppUser, long> _userRepo;

    public AuthenticationController(IMultiTenantHandler multiTenantHandler, IRepository<AppUser, long> userRepo)
    {
        _multiTenantHandler = multiTenantHandler;
        _userRepo = userRepo;
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
}