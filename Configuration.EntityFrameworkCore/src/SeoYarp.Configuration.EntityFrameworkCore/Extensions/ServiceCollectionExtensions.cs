using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ReverseProxy.Service;
using SeoYarp.Configuration.EntityFrameworkCore.Mappings;

namespace SeoYarp.Configuration.EntityFrameworkCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSeoYarp(this IServiceCollection services, Action<SeoYarpBuilder> builder)
        {
            var seoYarpBuilder = new SeoYarpBuilder(services);
            builder(seoYarpBuilder);
            seoYarpBuilder.Build();
            services.AddSingleton<IProxyConfigProvider>(sp => new EntityFrameworkConfigProvider(sp, seoYarpBuilder.PeriodicCheckInterval));
            return services;
        }

    }
}
