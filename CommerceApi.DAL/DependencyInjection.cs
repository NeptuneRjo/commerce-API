using CommerceApi.DAL.Repositories;
using CommerceApi.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceApi.DAL
{
    public static class DependencyInjection
    {
        public static void RegisterDALDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddDbContext<DataContext>(options =>
            {
                var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(defaultConnectionString);
            });
        }
    }
}
