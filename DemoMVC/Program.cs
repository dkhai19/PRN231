using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MySaleDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Category}/{action=Index}/{id?}");
//    endpoints.MapControllerRoute(
//        name: "products",
//        pattern: "Products/{action=Index}/{id?}",
//        defaults: new { controller = "Products" });
//});

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Category}/{action=Index}/{id?}"
);

app.Run();
