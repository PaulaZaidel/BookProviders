
using BookProviders.Business.Interfaces;
using BookProviders.Data.Context;
using BookProviders.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookProviders.App.Configurations
{
    public static class DependencyInjectionConfig 
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BookProvidersContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICatererRepository, CatererRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;
        }
    }
}
