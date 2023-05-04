#region using

using MediaExpertRecruitmentTask.Core.Mediator.Product.Query;
using MediaExpertRecruitmentTask.Database.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#endregion

namespace MediaExpertRecruitmentTask.Mediator.Product.Query;

public class
    GetProductListCommandHandler : IRequestHandler<GetProductListCommand, IEnumerable<Core.Model.Product.Product>>
{
    #region Constructor

    public GetProductListCommandHandler(
        DatabaseContext databaseContext,
        ILogger<GetProductListCommandHandler> logger)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

    #endregion

    #region Handle

    public async Task<IEnumerable<Core.Model.Product.Product>> Handle(
        GetProductListCommand request,
        CancellationToken cancellationToken)
    {
        return await Task.FromResult<IEnumerable<Core.Model.Product.Product>>(
            await _databaseContext.Product.ToListAsync(cancellationToken));
    }

    #endregion

    #region Properties

    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<GetProductListCommandHandler> _logger;

    #endregion
}