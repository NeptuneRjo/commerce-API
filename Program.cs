using CommerceClone.Data;
using CommerceClone.Interfaces;
using CommerceClone.Repository;
using CommerceClone.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

// Repositories
services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
services.AddScoped<IAdminRepository, AdminRepository>();
services.AddScoped<IItemRepository, ItemRepository>();
services.AddScoped<IStoreRepository, StoreRepository>();
services.AddScoped<ICartRepository, CartRepository>();

// Data
services.AddScoped<IDataContext, DataContext>();

// Services
services.AddScoped<IStoreService, StoreService>();
services.AddScoped<IAdminService, AdminService>();
services.AddScoped<ICartService, CartService>();
services.AddScoped<IItemService, ItemService>();

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
        opt.LoginPath = "/v1/auth";
        opt.LogoutPath = "/v1/signout";
    })
    .AddGitHub(opt =>
    {
        opt.ClientId = builder.Configuration["GitHub:ClientId"];
        opt.ClientSecret = builder.Configuration["GitHub:ClientSecret"];
    });

services.AddAutoMapper(typeof(Program));

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

app.MapControllers();

app.Run();
