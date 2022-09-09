using App.User.Dto;
using App.User.Services.Interfaces;
using App.Web.Manager.Interfaces;
using App.Web.ViewModel;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly IAuthManager _authManager;
    private readonly INotyfService _notyfService;
    private readonly IUserService _userService;

    public AuthController(IAuthManager authManager, INotyfService notyfService, IUserService userService)
    {
        _authManager = authManager;
        _notyfService = notyfService;
        _userService = userService;
    }
    
    public IActionResult Index() => View(new LoginVm());

    [HttpPost]
    public async Task<IActionResult> Index(LoginVm vm)
    {
        try
        {
            var result = await _authManager.Login(vm.Email, vm.Password);
            if (result.Success) return RedirectToAction("Index", "Home");
            ModelState.AddModelError(nameof(vm.Password), result.Errors.FirstOrDefault()!);
            vm.Password = "";
            return View(vm);
        }
        catch (Exception e)
        {
            _notyfService.Error(e.Message);
            return View();
        }
    }
    [HttpGet]
    public IActionResult Register() => View(new UserVm());

    [HttpPost]
    public async Task<IActionResult> Register(UserVm vm)
    {
        try
        {
            var userDto = new UserDto(vm.Name, vm.Gender, vm.Email, vm.Password, vm.Address, vm.Phone);
            await _userService.CreateUser(userDto);
            _notyfService.Success("User Created Successfully");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            _notyfService.Error(e.Message);
            return View();
        }
    }
}