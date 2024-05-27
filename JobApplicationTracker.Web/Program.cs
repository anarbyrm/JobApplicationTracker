using JobApplicationTracker.DataAccess;
using JobApplicationTracker.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// dependency injection from layers
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddApplicationDependencies();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
else
{
    // todo: add error page
    // todo: add page not found page
    app.UseExceptionHandler("/error");

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
