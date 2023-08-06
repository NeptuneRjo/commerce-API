using CommerceClone.Data;
using CommerceClone.Interfaces;
using CommerceClone.Repository;
using CommerceClone.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

// Repositories
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
    var azureConnectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
    options.UseSqlServer(azureConnectionString);
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
