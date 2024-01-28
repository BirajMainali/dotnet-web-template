#nullable enable
using Microsoft.AspNetCore.Mvc;
using NepaliDateConverter.Interfaces;

namespace App.Web.ViewComponents;

[ViewComponent(Name = "DateInputVc")]
public class DateInputVc : ViewComponent
{
    private readonly INepaliDateConverter _nepaliDateConverter;

    public DateInputVc(INepaliDateConverter nepaliDateConverter)
    {
        _nepaliDateConverter = nepaliDateConverter;
    }

    public IViewComponentResult Invoke(string name, DateTime? value, bool required = true, string id = "")
    {
        if (value.HasValue && value.Value == DateTime.MinValue)
        {
            value = null;
        }

        if (required && !value.HasValue)
        {
            value = DateTime.UtcNow;
        }

        var nepaliValue = "";
        if (value.HasValue)
        {
            nepaliValue = _nepaliDateConverter.GetBsDateFromAdDate(value.Value);
        }

        return View(new DateInputVm()
        {
            Name = name,
            Required = required,
            Value = nepaliValue,
            EnglishValue = value.HasValue ? value.Value.ToString("yyyy-MM-dd") : "",
            ElmId = id
        });
    }
}

public class DateInputVm
{
    public string Name { get; set; }
    public string Value { get; set; }
    public string EnglishValue { get; set; }
    public bool Required { get; set; }

    public string ElmId { get; set; }
}