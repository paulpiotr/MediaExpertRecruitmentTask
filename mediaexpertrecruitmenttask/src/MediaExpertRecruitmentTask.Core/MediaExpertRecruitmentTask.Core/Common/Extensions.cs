#region using

using Microsoft.Extensions.Configuration;

#endregion

namespace MediaExpertRecruitmentTask.Core.Common;

public static class Extensions
{
    public static TModel BindSectionAsModel<TModel>(this IConfiguration configuration, string section)
        where TModel : class, new()
    {
        var model = new TModel();

        configuration
            .GetSection(section)
            .Bind(model);

        return model;
    }

    public static TModel BindSectionAsModel<TModel>(string section)
        where TModel : class, new()
    {
        var model = new TModel();

        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build()
            .GetSection(section)
            .Bind(model);

        return model;
    }
}