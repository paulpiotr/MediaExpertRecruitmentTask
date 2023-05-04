#region using

using MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.ViewFeatures.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateRecaptchaAttribute : Attribute, IFilterFactory
{
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
        serviceProvider.GetService<ValidateRecaptchaActionFilter>() ?? new ValidateRecaptchaActionFilter();

    public bool IsReusable => true;
}
