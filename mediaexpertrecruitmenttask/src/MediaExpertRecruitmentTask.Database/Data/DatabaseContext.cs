#region using

using MediaExpertRecruitmentTask.Core.Data;
using MediaExpertRecruitmentTask.Core.Model.Product;
using MediaExpertRecruitmentTask.Database.Data.EntityTypeConfiguration.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MediaExpertRecruitmentTask.Database.Data;

public sealed class DatabaseContext : DbContext
{
    #region Constructor

    public DatabaseContext(
        DbContextOptions<DatabaseContext> options) : base(options)
    {
        _databaseFacade = Database;
    }

    #endregion

    #region DbSet

    public DbSet<Product> Product { get; set; } = null!;

    #endregion

    #region OnConfiguring

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            // Get database context settings
            var databaseContextSettings = Core.Common.Extensions.BindSectionAsModel<DatabaseContextSettings>(
                nameof(DatabaseContextSettings));

            options.UseSqlServer(databaseContextSettings.ConnectionString,
                x => x
                    .MigrationsHistoryTable(SqlServer.MigrationsHistoryTable,
                        SqlServer.MigrationsHistoryTableSchema)
                    .MigrationsAssembly(SqlServer.MigrationsAssembly));
        }
    }

    #endregion

    #region OnModelCreating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Get a static reference to the DatabaseFacade object
    /// </summary>
    /// <returns>
    ///     A static reference to the DatabaseFacade object
    /// </returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static DatabaseFacade GetDatabaseFacade()
    {
        return _databaseFacade;
    }

    #endregion

    #region Properties

    /// <summary>
    ///     A static reference to the DatabaseFacade object
    /// </summary>
    private static DatabaseFacade _databaseFacade = default!;

    /// <summary>
    ///     A reference to the IServiceCollection object
    /// </summary>
    private readonly IServiceCollection? _services;

    #endregion
}