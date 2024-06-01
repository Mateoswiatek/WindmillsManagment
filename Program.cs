using MgWindManager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWindmillServices, WindmillServices>();

builder.Services.AddDbContext<MgWindCtx>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("windmillsManagement"));
});

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); // wywala nam całe wyjątki
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// public static IApplicationBuilder CreateDatabase<TDatabase>(this IApplicationBuilder app) where TDatabase : DbContext
// {
//     using var scope = app.ApplicationServices.CreateScope();
//
//     var dbContext = scope.ServiceProvider.GetRequiredService<TDatabase>();
//     dbContext.Database.EnsureCreated();
//
//     return app;
// }