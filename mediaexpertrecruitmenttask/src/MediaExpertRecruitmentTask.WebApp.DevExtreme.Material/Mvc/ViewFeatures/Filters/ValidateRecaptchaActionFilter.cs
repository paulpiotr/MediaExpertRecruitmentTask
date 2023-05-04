#region using

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;

#endregion

namespace MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Mvc.ViewFeatures.Filters;

public class ValidateRecaptchaActionFilter : IActionFilter
{
    #region Methods

    #region OnActionExecuting

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Check context is not null
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        // Get Logger from service
        _logger =
            (ILogger<ValidateRecaptchaAttributeActionFilter>)context.HttpContext.RequestServices.GetService(
                typeof(ILogger<ValidateRecaptchaAttributeActionFilter>))!;

        // Get Configuration from service
        _config = (IConfiguration?)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));

        // Check configuration and log warn if not set
        if (_config == null || (string.IsNullOrWhiteSpace(_config?.GetSection("RecaptchaSiteKey").Value) &&
                                string.IsNullOrWhiteSpace(_config?.GetSection("RecaptchaSecretKey").Value)))
        {
            _logger?.LogWarning(
                @"For use Recaptcha set RecaptchaSiteKey and RecaptchaSecretKey in 'appsettings.json' file!");
            return;
        }

        // Get Controller
        _controller = (Controller?)context.Controller;

        // Check Controller is not null
        if (_controller == null)
        {
            throw new ArgumentNullException(nameof(_controller));
        }

        // Get RecaptchaVerificationHelper
        RecaptchaVerificationHelper? recaptchaHelper = _controller?.GetRecaptchaVerificationHelper();

        // Check RecaptchaVerificationHelper is not null
        if (recaptchaHelper == null)
        {
            throw new ArgumentNullException(nameof(recaptchaHelper));
        }

        // Set dictionary of first element in context model
        Dictionary<string, object>? dictionary =
            JsonConvert.DeserializeObject<Dictionary<string, object>>(
                string.Empty
                //context.ModelState.Values.FirstOrDefault()?.SerializeToString()!
                );

        // Validate RecaptchaVerificationHelper response is not empty
        if (string.IsNullOrEmpty(recaptchaHelper.Response))
        {
            context.ModelState.AddModelError(
                (dictionary != null && dictionary.ContainsKey("Key") && dictionary.TryGetValue("Key", out var @object)
                    ? @object.ToString()
                    : null)!,
                "Captcha answer cannot be empty!");
            return;
        }

        // Validate RecaptchaVerificationHelper response is valid
        RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
        if (!recaptchaResult.Success)
        {
            context.ModelState.AddModelError(
                (dictionary != null && dictionary.ContainsKey("Key") && dictionary.TryGetValue("Key", out var @object)
                    ? @object.ToString()
                    : null)!,
                "Incorrect captcha answer!");
        }
    }

    #endregion

    #region OnActionExecuted

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    #endregion

    #endregion

    #region Properties

    /// <summary>
    /// </summary>
    private Controller? _controller;

    /// <summary>
    /// </summary>
    private ILogger<ValidateRecaptchaAttributeActionFilter>? _logger;

    /// <summary>
    /// </summary>
    private IConfiguration? _config;

    #endregion
}
