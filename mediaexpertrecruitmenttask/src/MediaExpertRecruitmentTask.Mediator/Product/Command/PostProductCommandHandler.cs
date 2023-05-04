#region using

using MediaExpertRecruitmentTask.Core.Mediator.Product.Command;
using MediaExpertRecruitmentTask.Database.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#endregion

namespace MediaExpertRecruitmentTask.Mediator.Product.Command;

public class PostProductCommandHandler : IRequestHandler<PostProductCommand, Unit>
{
    #region Constructor

    public PostProductCommandHandler(
        ILogger<PostProductCommandHandler> logger,
        DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    #endregion

    #region Handle

    public async Task<Unit> Handle(PostProductCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.Model == null)
        {
            throw new NullReferenceException(nameof(request.Model));
        }

        if (!request.Model.Id.Equals(Guid.Empty)
            && await _databaseContext.Product
                .AsNoTracking()
                .AsNoTrackingWithIdentityResolution()
                .IgnoreAutoIncludes()
                .AnyAsync(a =>
                    !a.Id.Equals(request.Model.Id)
                    && a.Code.Equals(request.Model.Code), cancellationToken))
        {
            throw new InvalidOperationException("" +
                                                $"{nameof(Core.Model.Product.Product)} " +
                                                $"by {nameof(Core.Model.Product.Product.Code)} as \"{request.Model.Code}\" " +
                                                "exists! " +
                                                $"Type other {nameof(Core.Model.Product.Product.Code)}!");
        }

        _databaseContext.Entry(request.Model).State =
            request.Model.Id.Equals(Guid.Empty) ? EntityState.Added : EntityState.Modified;

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(new Unit());
    }

    #endregion

    #region Properties

    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<PostProductCommandHandler> _logger;

    #endregion
}