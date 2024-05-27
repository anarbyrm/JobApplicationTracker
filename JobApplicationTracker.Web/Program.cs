using JobApplicationTracker.DataAccess;
using JobApplicationTracker.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// dependency injection from layers
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddApplicationDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapFallbackToController("ItemOrPageNotFound", "Error");
});

app.Run();
