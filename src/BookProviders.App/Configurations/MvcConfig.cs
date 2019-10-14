using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BookProviders.App.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(options =>
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "Invalid Field")
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return services;
        }
    }
}
