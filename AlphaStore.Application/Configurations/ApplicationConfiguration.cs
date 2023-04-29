using AlphaStore.Application.Services.Implementations;
using AlphaStore.Application.Services.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AlphaStore.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddAutoMapper(
                Assembly.GetExecutingAssembly(),
                Assembly.GetEntryAssembly(),
                Assembly.GetCallingAssembly());

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
