using System.Diagnostics;
using App.Base.Repository;
using App.User.Entity;
using App.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<AppUser, long> _userRepo;

    public HomeController(ILogger<HomeController> logger, IRepository<AppUser, long> userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepo.GetQueryable().Select(x=> x.Email).ToListAsync();
        return View(users);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}