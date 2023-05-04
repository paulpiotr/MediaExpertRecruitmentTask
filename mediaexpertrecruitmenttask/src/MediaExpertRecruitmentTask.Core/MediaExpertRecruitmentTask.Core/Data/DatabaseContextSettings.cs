namespace MediaExpertRecruitmentTask.Core.Data;

public class DatabaseContextSettings
{
    /// <summary>
    ///     A const string of default schema name
    /// </summary>
    public const string DefaultSchema = "mert";

    /// <summary>
    ///     A const string of default migrations table name
    /// </summary>
    public const string DefaultMigrationsHistoryTable = "__EFMigrationsHistory";

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? ConnectionString { get; set; }
}