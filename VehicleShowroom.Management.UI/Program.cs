using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Serilog;
using VehicleShowroom.Mangement.Infrastructure.Configuration;

var builder         = WebApplication.CreateBuilder(args);
var services        = builder.Services;
var configuration   = builder.Configuration;

builder.AddSerilog();

services.ConfigureIdentity(configuration);
services.AddDependencyInjection();
services.AddAutoMapper();
// Add services to the container.
services.AddHttpClient();
services.AddControllersWithViews().AddRazorOptions(opts =>
{
    opts.ViewLocationFormats.Add("/{0}.cshtml");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.SameAsRequest
});
app.UseAuthentication();
app.UseAuthorization();

app.UseNToastNotify();
app.UseNotyf();

app.MapControllers();
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();