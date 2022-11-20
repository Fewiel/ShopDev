using ShopDev.DAL.Models;
using ShopDev.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#if DEBUG
var database = new Database($"server=shopdev.local;database=shopdev;uid=shopdev;pwd=ShopDev2022#; convert zero datetime=True;");
#endif

database.ConfigureMigrate(builder.Services);
database.ConfigureRepositories(builder.Services);

var app = builder.Build();

database.Migrate(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();