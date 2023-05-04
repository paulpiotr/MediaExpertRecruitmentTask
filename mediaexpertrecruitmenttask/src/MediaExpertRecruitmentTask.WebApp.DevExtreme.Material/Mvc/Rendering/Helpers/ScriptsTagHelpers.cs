#region using

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.Rendering.Helpers;

/// <summary>
///     Helper Display script in end of body
/// </summary>
public static class ScriptsTagHelpers
{
    #region public static HtmlString Script(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)

    /// <summary>
    ///     Add script to HttpContext
    ///     Add in top on .cshtml:
    ///     @using WebApplicationNetCoreDev.Helpers
    ///     Add script as:
    ///     @Html.Script(
    ///     @
    ///     <script type="text/javascript">
    ///         $(function () {
    ///         });
    ///     </script>
    ///     )
    /// </summary>
    /// <param name="htmlHelper">IHtmlHelper htmlHelper</param>
    /// <param name="template">HtmlString.Empty</param>
    /// <returns>
    ///     HtmlString.Empty
    /// </returns>
    public static HtmlString Script(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)
    {
        htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
        return HtmlString.Empty;
    }

    #endregion

    #region public static HtmlString RenderScripts(this IHtmlHelper htmlHelper)

    /// <summary>
    ///     Display script in bottom main page or other template
    ///     Add in top on .cshtml:
    ///     @using WebApplicationNetCoreDev.Helpers
    ///     And add in bottom section example:
    ///     @Html.RenderScripts()
    /// </summary>
    /// <param name="htmlHelper">
    ///     IHtmlHelper htmlHelper
    /// </param>
    /// <returns>
    ///     HtmlString.Empty
    /// </returns>
    public static HtmlString RenderScripts(this IHtmlHelper htmlHelper)
    {
        foreach (var @object in from object key in htmlHelper.ViewContext.HttpContext.Items.Keys
                 let keyString = key.ToString()
                 where null != keyString && keyString.StartsWith("_script_")
                 select new { key })
        {
            if (htmlHelper.ViewContext.HttpContext.Items[@object.key] is Func<object, HelperResult> template)
            {
                htmlHelper.ViewContext.Writer.Write(template(null!));
            }
        }

        return HtmlString.Empty;
    }

    #endregion
}
