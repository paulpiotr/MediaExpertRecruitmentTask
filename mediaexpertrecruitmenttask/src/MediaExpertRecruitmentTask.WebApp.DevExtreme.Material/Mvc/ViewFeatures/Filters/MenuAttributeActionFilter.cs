#region using

using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.ViewFeatures.Filters;

public class MenuAttributeActionFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) =>
        new MenuActionFilter().OnActionExecuting(context);

    public void OnActionExecuted(ActionExecutedContext context) => new MenuActionFilter().OnActionExecuted(context);
}
