using CommerceApi.BLL.Interfaces;
using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities.AutoMapperProfiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommerceApi.BLL
{
    public static class DependencyInjection
    {
        public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration Configuration) {
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IItemService, ItemService>();
        }
    }
}
