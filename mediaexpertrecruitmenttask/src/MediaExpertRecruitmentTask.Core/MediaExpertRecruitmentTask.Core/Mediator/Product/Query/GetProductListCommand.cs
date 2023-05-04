#region using

using MediatR;

#endregion

namespace MediaExpertRecruitmentTask.Core.Mediator.Product.Query;

public class GetProductListCommand : IRequest<IEnumerable<Model.Product.Product>>
{
}