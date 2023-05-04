#region using

using MediatR;

#endregion

namespace MediaExpertRecruitmentTask.Core.Mediator.Product.Command;

public class PostProductCommand : IRequest<Unit>
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public Model.Product.Product? Model { get; set; }
}