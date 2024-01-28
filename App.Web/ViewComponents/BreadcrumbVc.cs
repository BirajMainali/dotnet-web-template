using Microsoft.AspNetCore.Mvc;

namespace App.Web.ViewComponents;

[ViewComponent(Name = "BreadcrumbVc")]
public class BreadcrumbVc : ViewComponent
{
    public IViewComponentResult Invoke(string crumbs)
    {
        var parts = crumbs.Split(new[] { '>', '/' });
        return View(parts);
    }
}