using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace EventOrganizer.Helpers
{
    public static class PagingHelper
    {
        public static string PageLinks(PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml.AppendHtml(i.ToString());
                if (i != pageInfo.CurrentPageNumber)
                {
                    tag.AddCssClass("btn-default");
                }
                tag.AddCssClass("btn");
                using (var writer = new System.IO.StringWriter())
                {
                    tag.WriteTo(writer, HtmlEncoder.Default);
                    result.Append(writer.ToString());
                }
            }
            return result.ToString();
        }
    }
}
