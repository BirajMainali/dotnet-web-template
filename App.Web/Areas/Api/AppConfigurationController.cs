using App.Base.Extensions;
using App.Base.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace App.Web.Areas.Api;

[ApiController]
[Area("Api")]
[Route("[area]/[controller]")]
public class AppConfigurationController : ControllerBase
{
    private readonly IOptions<AppSettings> _options;

    public AppConfigurationController(IOptions<AppSettings> options)
    {
        _options = options;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return this.SendSuccess("App configuration", _options.Value);
    }
}