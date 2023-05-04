#region using

using System.Reflection;
using MediaExpertRecruitmentTask.Database.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MediaExpertRecruitmentTask.Mediator.Extensions.DependencyInjection;

public static class MediaExpertRecruitmentTaskMediatorExtension
{
    public static IServiceCollection AddMediaExpertRecruitmentTaskMediator(this IServiceCollection services)
    {
        // Throw if services is null
        if (services == default!)
        {
            throw new ArgumentNullException(nameof(services));
        }

        return services
            .AddMediaExpertRecruitmentTaskDatabase()
            .AddMediatR(configuration => configuration
                .RegisterServicesFromAssembly(Assembly.GetCallingAssembly())
                .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddTransient(typeof(IMediator), typeof(MediatR.Mediator));
    }
}