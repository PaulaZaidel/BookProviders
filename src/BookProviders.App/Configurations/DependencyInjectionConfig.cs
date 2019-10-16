
using BookProviders.Business.Interfaces;
using BookProviders.Business.Notifications;
using BookProviders.Business.Services;
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

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICatererService, CatererService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
