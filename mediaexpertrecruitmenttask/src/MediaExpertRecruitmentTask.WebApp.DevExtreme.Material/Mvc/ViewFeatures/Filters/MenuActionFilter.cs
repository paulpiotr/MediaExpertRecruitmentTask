#region using

using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.ViewFeatures.Filters;

public class MenuActionFilter : IActionFilter
{
    //#region Properties

    ///// <summary>
    /////     IMediator instance from service
    ///// </summary>
    //private IMediator? _mediator;

    //#endregion

    #region Methods

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine(nameof(OnActionExecuting));

        //// Check context is not null
        //if (context == null)
        //{
        //    throw new ArgumentNullException(nameof(context));
        //}

        //// Get Mediator from service
        //_mediator = (IMediator?)context.HttpContext.RequestServices.GetService(typeof(IMediator));

        //// Check Mediator is not null
        //if (_mediator == null)
        //{
        //    throw new ArgumentNullException(nameof(_mediator));
        //}

        //// Publish notification
        //_mediator?
        //    .Publish(new ActionExecutingContextEvent(context))
        //    .Wait();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    #endregion
}
