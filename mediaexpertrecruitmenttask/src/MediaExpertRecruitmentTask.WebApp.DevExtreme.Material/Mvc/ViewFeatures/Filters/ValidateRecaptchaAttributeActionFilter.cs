#region using

using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.ViewFeatures.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateRecaptchaAttributeActionFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) =>
        new ValidateRecaptchaActionFilter().OnActionExecuting(context);

    public void OnActionExecuted(ActionExecutedContext context) =>
        new ValidateRecaptchaActionFilter().OnActionExecuted(context);
}
