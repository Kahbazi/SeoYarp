using System;
using Microsoft.ReverseProxy.Service;
using SeoYarp.Configuration.EntityFrameworkCore.Interfaces;

namespace SeoYarp.Configuration.EntityFrameworkCore
{
    public class EntityFrameworkConfigProvider : IProxyConfigProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private TimeSpan? _periodicCheckInterval;

        public EntityFrameworkConfigProvider(IServiceProvider serviceProvider, TimeSpan? periodicCheckInterval)
        {
            _serviceProvider = serviceProvider;
            _periodicCheckInterval = periodicCheckInterval;
        }

        public IProxyConfig GetConfig()
        {
            throw new NotImplementedException();
        }
    }
}
