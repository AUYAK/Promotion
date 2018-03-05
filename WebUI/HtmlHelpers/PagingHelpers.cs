using System;
using System.Text;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper helper, PagingInfo paginginfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= paginginfo.TotalPages; i++)
            {
                TagBuilder tag_a = new TagBuilder("a");
                tag_a.MergeAttribute("href", pageUrl(i));
                tag_a.InnerHtml = i.ToString();
                if (i == paginginfo.CurrentPage) tag_a.AddCssClass("selected");
                result.Append(tag_a.ToString());
            }
            return MvcHtmlString.Create(result.ToString());

        }

    }
}