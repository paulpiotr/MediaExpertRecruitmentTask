#region using

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

#endregion

namespace MediaExpertRecruitmentTask.Database.Data;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    #region public DbContext CreateDbContext(string[] args)

    /// <summary>
    ///     Creates a new instance of a DbContext.
    /// </summary>
    /// <param name="args"> Arguments provided by the design-time service. </param>
    /// <returns> An instance of DbContext. </returns>
    public DatabaseContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>();
        //if (!string.IsNullOrWhiteSpace(connectionString))
        //{
        //    switch (connectionProviderName)
        //    {
        //        default:
        //            options.UseSqlServer(connectionString,
        //                x =>
        //                    x.MigrationsHistoryTable(
        //                            SqlServer.MigrationsHistoryTable,
        //                            SqlServer.MigrationsHistoryTableSchema)
        //                        .MigrationsAssembly(SqlServer.MigrationsAssembly));
        //            break;
        //    }
        //}

        return new DatabaseContext(options.Options);

        //return new DatabaseContext(options.Options, default!);
    }

    #endregion
}