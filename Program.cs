using MgWindManager;
using MgWindManager.Models;
using MgWindManager.Services;
using MgWindManager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IWindmillService, WindmillService>();
builder.Services.AddScoped<IWindParkService, WindParkService>();

builder.Services.AddScoped<IWeatherApiService, WeatherApiService>();

builder.Services.AddLogging(b =>
{
    b.AddConsole();
});

builder.Services.AddDbContext<MgWindCtx>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("windmillsManagement"));
});

//Autentyfikacja, Logowanie.
builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(2); // Czas blokady konta
    options.Lockout.MaxFailedAccessAttempts = 5; // Maksymalna liczba nieudanych prób logowania przed zablokowaniem konta
    
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 2;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<MgWindCtx>();
//todo zmienić tak, aby baza danych userów nie była w tej samej bazce co cała aplikacja.

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

app.UseAuthentication();
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