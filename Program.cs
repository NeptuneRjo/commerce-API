using CommerceClone.Data;
using CommerceClone.Interfaces;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

services.AddScoped<ICartRepository, CartRepository>();
services.AddScoped<IItemRepository, ItemRepository>();
services.AddScoped<IStoreRepository, StoreRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IDataContext, DataContext>();
services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

services.AddAuthentication(opt =>
{
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(opt =>
    {
        opt.LoginPath = "/signin";
        opt.LogoutPath = "/signout";
    })
    .AddGitHub(opt =>
    {
        opt.ClientId = builder.Configuration["GitHub:ClientId"];
        opt.ClientSecret = builder.Configuration["GitHub:ClientSecret"];
    });
    //.AddGoogle(opt =>
    //{
    //    opt.ClientId = builder.Configuration["Google:ClientId"];
    //    opt.ClientSecret = builder.Configuration["Google:ClientSecret"];
    //});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
