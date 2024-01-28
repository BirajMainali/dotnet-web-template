using Microsoft.AspNetCore.Razor.TagHelpers;
using NepaliDateConverter.Interfaces;

namespace App.Web.CustomTagHelpers
{
    public class MultiDateSpanTagHelper : TagHelper
    {
        private readonly INepaliDateConverter _dateConverter;

        public MultiDateSpanTagHelper(INepaliDateConverter dateConverter)
        {
            _dateConverter = dateConverter;
        }

        public string Value { get; set; }
        public bool ShowTime { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var date = Convert.ToDateTime(Value);
            if (!ShowTime) date = date.Date;

            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            var nepaliDate = _dateConverter.GetBsDateFromAdDate(date.Year, date.Month, date.Day);
            string outputContent = nepaliDate;
            output.Content.SetContent(outputContent);
        }
    }
}