#region using

using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Extensions.DependencyInjection;

public static class WebAppDevExtremeMaterialExtension
{
    public static IServiceCollection AddWebAppDevExtremeMaterial(
        this IServiceCollection services)
    {
        // Throw if services is null
        if (services == default!)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Add razor pages
        services.AddRazorPages();


        services
            .AddControllersWithViews(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
            //    {
            //        NamingStrategy = new CamelCaseNamingStrategy
            //        {
            //            ProcessDictionaryKeys = true,
            //            OverrideSpecifiedNames = false
            //        }
            //    };
            //})
            ;

        return services;
    }
}
