#region using

using MediaExpertRecruitmentTask.Core.Mediator.Product.Command;
using MediaExpertRecruitmentTask.Core.Mediator.Product.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

#endregion

namespace MediaExpertRecruitmentTask.WebApi.Areas.Product.Controllers;

[Area(AreaName)]
[Tags(AreaName)]
[Route("api/[area]/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    #region Constructor

    public ProductController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    private readonly IMediator _mediator;

    #endregion

    #region Metods

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Core.Model.Product.Product>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<Core.Model.Product.Product>> Get(CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetProductListCommand(), cancellationToken);
    }

    [HttpPost(PostFromFormActionName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Core.Model.Product.Product>>> PostFromForm(
        [FromForm] string values,
        CancellationToken cancellationToken = default)
    {
        var model = new Core.Model.Product.Product();
        JsonConvert.PopulateObject(values, model);
        return await PostFromBody(model, cancellationToken);
    }

    [HttpPost(PostFromBodyActionName)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Core.Model.Product.Product>>> PostFromBody(
        [FromBody] Core.Model.Product.Product model,
        CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _mediator.Send(new PostProductCommand { Model = model }, cancellationToken);
        return NoContent();
    }

    #endregion

    #region Constants

    /// <summary>
    ///     Nazwa obszaru
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string AreaName = "Product";

    /// <summary>
    ///     Nazwa kontrolera
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public const string ControllerName = "Product";

    /// <summary>
    ///     Nazwa metody Get
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string GetActionName = "Get";

    /// <summary>
    ///     Nazwa metody PostFromForm
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string PostFromFormActionName = "PostFromForm";

    /// <summary>
    ///     Nazwa metody PostFromBody
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    public const string PostFromBodyActionName = "PostFromBody";

    #endregion
}