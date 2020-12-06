using Microsoft.Extensions.DependencyInjection;
using SeoYarp.Configuration.EntityFrameworkCore.Mappings;

namespace SeoYarp.Configuration.EntityFrameworkCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultEntityMapper(this IServiceCollection services)
        {
            return services.AddScoped<IMapper, DefaultMapper>();
        }
    }
}
