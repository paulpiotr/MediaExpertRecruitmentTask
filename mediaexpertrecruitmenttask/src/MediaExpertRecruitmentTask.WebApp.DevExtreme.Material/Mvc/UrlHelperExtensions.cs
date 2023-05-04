#region using

using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc;

public static class UrlHelperExtensions
{
    public static string RouteUrl(
        this IUrlHelper urlHelper,
        string routeName,
        string area,
        string controller,
        string action,
        string? culture = "pl-PL",
        string? returnUrl = default!)
    {
        // Validate IUrlHelper
        if (urlHelper == null)
        {
            throw new ArgumentNullException(nameof(urlHelper));
        }

        // Return result
        return urlHelper.RouteUrl(routeName, new
        {
            culture = culture ?? GetCulture(urlHelper),
            area,
            controller,
            action,
            returnUrl
        }) ?? string.Empty;
    }

    public static async Task<string> RouteUrlAsync(
        this IUrlHelper urlHelper,
        string routeName,
        string area,
        string controller,
        string action,
        string? culture = "pl-PL",
        string? returnUrl = default!,
        CancellationToken cancellationToken = default) =>
        await Task.Run(() => RouteUrl(urlHelper, routeName, area, controller, action, culture, returnUrl),
            cancellationToken);

    public static string GetReturnUrl(this IUrlHelper urlHelper)
    {
        // Validate IUrlHelper
        if (urlHelper == null)
        {
            throw new ArgumentNullException(nameof(urlHelper));
        }

        // Validate HttpContext
        if (urlHelper.ActionContext is { HttpContext: null })
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException(typeof(HttpContext).ToString());
            }
        }

        // Check request query by "returnUrl" key
        if (urlHelper.ActionContext.HttpContext!.Request.Query.TryGetValue("returnUrl",
                out StringValues stringValues) && !string.IsNullOrWhiteSpace(stringValues.ToString()))
        {
            if (urlHelper.IsLocalUrl(stringValues.ToString()))
            {
                return stringValues.ToString();
            }
        }

        // Check request query by "ReturnUrl" key
        if (urlHelper.ActionContext.HttpContext.Request.Query.TryGetValue("ReturnUrl",
                out stringValues) && !string.IsNullOrWhiteSpace(stringValues.ToString()))
        {
            if (urlHelper.IsLocalUrl(stringValues.ToString()))
            {
                return stringValues.ToString();
            }
        }

        // Return default Home Url
        return RouteUrl(urlHelper, "DefaultApp", "Home", "Home", "Index");
    }

    public static async Task<string> GetReturnUrlAsync(this IUrlHelper urlHelper,
        CancellationToken cancellationToken = default) =>
        await Task.Run(() => GetReturnUrl(urlHelper), cancellationToken);

    public static string GetReturnUrl<TModel>(this IUrlHelper urlHelper, TModel model) => string.Empty;

    public static ISession? TryGetSession(this HttpContext? httpContext)
    {
        ISession? session = default!;
        try
        {
            session = httpContext?.Session;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

        return session != default! ? session : default!;
    }

    public static async Task<string> GetCultureAsync(this IUrlHelper urlHelper, string culture = "PL-pl",
        CancellationToken cancellationToken = default) =>
        await Task.Run(() => GetCulture(urlHelper, culture), cancellationToken);


    public static string GetCulture(this IUrlHelper urlHelper, string culture = "PL-pl")
    {
        // Validate IUrlHelper
        if (urlHelper == null)
        {
            throw new ArgumentNullException(nameof(urlHelper));
        }

        // Validate HttpContext
        if (urlHelper.ActionContext is { HttpContext: null })
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException(typeof(HttpContext).ToString());
            }
        }

        // Try get ISession
        ISession? session = urlHelper.ActionContext.HttpContext!.TryGetSession();

        // Get culture from route values
        if (urlHelper.ActionContext.HttpContext!.Request.RouteValues.TryGetValue("culture", out var @object) &&
            @object != null && !string.IsNullOrWhiteSpace(@object.ToString()))
        {
            culture = @object.ToString()!;
        }

        // Set culture if from request query
        else if (urlHelper.ActionContext.HttpContext.Request.Query.TryGetValue("culture", out StringValues value) &&
                 !string.IsNullOrWhiteSpace(value.ToString()))
        {
            culture = value.ToString();
        }

        // Set culture from session variable
        else if (session != default!)
        {
            if (session.TryGetValue("culture", out var arrayBytes))
            {
                var result = Encoding.UTF8.GetString(arrayBytes);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    culture = result;
                }
            }
        }

        return culture;
    }
}
