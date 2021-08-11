using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Web.CustomHtmlHelper
{
    public static class CustomHtmlHelpers
    {

        public static IHtmlContent zLabel(this IHtmlHelper htmlHelper, string lblName)
        {
            return new HtmlString($"<label asp-for={lblName} class='label-control'>{lblName}</label>");
        }

        public static IHtmlContent zInput(this IHtmlHelper htmlHelper, string value, string name, string placeholder)
        {
            string str = $"<input type='Text' name='{name}'  class='form-control' placeholder='{placeholder}'/>";
            return new HtmlString(str);
        }

        public static IHtmlContent zDropdownlist(this IHtmlHelper htmlHelper, IEnumerable<SelectListItem> list)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<select class='form-control'>");
            foreach (var item in list)
            {
                stringBuilder.AppendFormat("<option value='{0}' {2}>{1}</option>", item.Value, item.Text, (item.Selected ? "selected" : ""));
            }
            stringBuilder.Append("</select>");

            return new HtmlString(stringBuilder.ToString());
        }



    }
}
