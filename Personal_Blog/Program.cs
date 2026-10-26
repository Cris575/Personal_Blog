using Microsoft.AspNetCore.Authentication.Cookies;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;
using Personal_Blog.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/";
        options.AccessDeniedPath = "/Home/";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IArticle, RepositoryDataJson>();
builder.Services.AddTransient<ILogin, RepositoryDataJsonUser>();
builder.Services.AddTransient<Articles, Articles>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        // pattern: "{controller=Home}/{action=Index}/{id?}")
        pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();