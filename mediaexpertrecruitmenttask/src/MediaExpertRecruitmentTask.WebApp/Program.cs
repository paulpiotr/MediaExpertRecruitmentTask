#region using

using MediaExpertRecruitmentTask.Core.Common;
using MediaExpertRecruitmentTask.Mediator.Extensions.DependencyInjection;
using MediaExpertRecruitmentTask.WebApp.DevExtreme.Material.Extensions.DependencyInjection;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediaExpertRecruitmentTaskMediator();
builder.Services.AddWebAppDevExtremeMaterial();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(MapControllerRoute.ConstDefaultApp,
    "{area=Home}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(MapControllerRoute.ConstDefaultApi,
    "api/{area=Home}/{controller=Home}/{action=Index}/{id?}");

app.Run();