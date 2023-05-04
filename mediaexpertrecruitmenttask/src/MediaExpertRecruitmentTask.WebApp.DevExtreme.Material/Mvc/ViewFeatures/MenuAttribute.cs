#region using

using MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.ViewFeatures.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.ViewFeatures;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MenuAttribute : Attribute, IFilterFactory
{
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
        serviceProvider.GetService<MenuActionFilter>() ?? new MenuActionFilter();

    public bool IsReusable => true;
}
