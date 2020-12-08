using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ReverseProxy.Service;
using SeoYarp.Configuration.EntityFrameworkCore.Mappings;
using SeoYarp.Configuration.EntityFrameworkCore.Options;

namespace SeoYarp.Configuration.EntityFrameworkCore
{
    public class SeoYarpBuilder
    {
        private readonly IServiceCollection _serviceCollection;
        private bool _mappedAdded = false;
        private bool _dbContextAdded = false;
        private TimeSpan? _periodicCheckInterval;
        private ReverseProxyStoreOptions _reverseProxyStoreOptions;

        public TimeSpan? PeriodicCheckInterval => _periodicCheckInterval;


        public SeoYarpBuilder(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public SeoYarpBuilder AddDefaultMapper()
        {
            _serviceCollection.AddSingleton<IMapper, DefaultMapper>();
            _mappedAdded = true;
            return this;
        }

        public SeoYarpBuilder AddMapper<T>(T mapper) where T : class, IMapper
        {
            _serviceCollection.AddSingleton(typeof(IMapper), typeof(T));
            _mappedAdded = true;
            return this;
        }

        public SeoYarpBuilder AddPeriodicCheck(TimeSpan timeSpan)
        {
            _periodicCheckInterval = timeSpan;
            return this;
        }

        public SeoYarpBuilder AddOptions(ReverseProxyStoreOptions options)
        {
            _reverseProxyStoreOptions = options;
            return this;
        }

        public SeoYarpBuilder AddDbContext(Action<DbContextOptionsBuilder> builder)
        {
            _reverseProxyStoreOptions ??= new ReverseProxyStoreOptions();
            _reverseProxyStoreOptions.ConfigureDbContext = builder;
            _dbContextAdded = true;
            return this;
        }

        public SeoYarpBuilder AddDbContext<T>(Action<DbContextOptionsBuilder<T>> builder) where T: DbContext
        {
            _reverseProxyStoreOptions ??= new ReverseProxyStoreOptions();
            _reverseProxyStoreOptions.ConfigureDbContext = builder as Action<DbContextOptionsBuilder>;
            _dbContextAdded = true;
            return this;
        }

        internal void Build()
        {
            if (!_mappedAdded)
            {
                throw new Exception("No Mapper has been provided for SeoYarp, please refer to documentation to see how to add mapping");
            }

            if (!_dbContextAdded)
            {
                throw new Exception("No DbContext has been added to SeoYarp, please refer to documentation to see how to add DbContext");
            }
        }


    }
}
