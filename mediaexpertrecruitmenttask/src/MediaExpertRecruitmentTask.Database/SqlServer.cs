#region using

using MediaExpertRecruitmentTask.Core.Data;

#endregion

namespace MediaExpertRecruitmentTask.Database;

public static class SqlServer
{
    public static readonly string MigrationsHistoryTable = DatabaseContextSettings.DefaultMigrationsHistoryTable;
    public static readonly string MigrationsHistoryTableSchema = DatabaseContextSettings.DefaultSchema;
    public static readonly string MigrationsAssembly = "MediaExpertRecruitmentTask.Migrations.SqlServer";
}