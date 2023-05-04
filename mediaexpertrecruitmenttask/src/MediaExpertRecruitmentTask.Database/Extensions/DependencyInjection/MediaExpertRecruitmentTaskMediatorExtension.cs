#region using

using MediaExpertRecruitmentTask.Core.Data;
using MediaExpertRecruitmentTask.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MediaExpertRecruitmentTask.Database.Extensions.DependencyInjection;

public static class MediaExpertRecruitmentTaskMediatorExtension
{
    public static IServiceCollection AddMediaExpertRecruitmentTaskDatabase(this IServiceCollection services)
    {
        // Throw if services is null
        if (services == default!)
        {
            throw new ArgumentNullException(nameof(services));
        }

        // Get database context settings
        var databaseContextSettings = Core.Common.Extensions.BindSectionAsModel<DatabaseContextSettings>(
            nameof(DatabaseContextSettings));

        // Add database context pool
        if (services.All(w => w.ServiceType != typeof(DatabaseContext)))
        {
            services.AddDbContextPool<DatabaseContext>(
                options => options.UseSqlServer(databaseContextSettings.ConnectionString,
                    x => x
                        .MigrationsHistoryTable(SqlServer.MigrationsHistoryTable,
                            SqlServer.MigrationsHistoryTableSchema)
                        .MigrationsAssembly(SqlServer.MigrationsAssembly))
            );
        }

        // Migrate
        using var buildServiceProvider = services.BuildServiceProvider();
        using var databaseContext = buildServiceProvider.GetRequiredService<DatabaseContext>();
        databaseContext.Database.Migrate();
        buildServiceProvider.Dispose();
        databaseContext.Dispose();

        return services;
    }
}