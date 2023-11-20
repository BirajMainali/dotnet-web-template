using App.User.Dto;
using App.User.Repositories.Interfaces;
using App.User.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers;

[AllowAnonymous]
public class SeedController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepo;

    public SeedController(IUserService userService, IUserRepository userRepo)
    {
        _userService = userService;
        _userRepo = userRepo;
    }

    public async Task<IActionResult> SeedAdmin()
    {
        try
        {
            if (await _userRepo.CheckIfExistAsync(x => x.Email == "admin@gmail.com"))
            {
                throw new Exception("Admin already exist");
            }

            var dto = new UserDto("Admin", "Male", "admin@gmail.com", "admin@123", "Mechi, Nepal", "100000000");
            var result = await _userService.CreateUser(dto);
            if (result.IsSuccess)
            {
                return Ok(new
                {
                    message = "Success..."
                });
            }

            return BadRequest(result.Error);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}