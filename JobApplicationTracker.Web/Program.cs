using JobApplicationTracker.DataAccess;
using JobApplicationTracker.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// dependency injection from layers
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddApplicationDependencies();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
