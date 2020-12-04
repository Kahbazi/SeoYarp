using System;
using Microsoft.ReverseProxy.Service;
using SeoYarp.Configuration.EntityFrameworkCore.Interfaces;

namespace SeoYarp.Configuration.EntityFrameworkCore
{
    public class EntityFrameworkConfigProvider : IProxyConfigProvider
    {
        private readonly IReverseProxyDbContext _reverseProxyDbContext;

        public EntityFrameworkConfigProvider(IReverseProxyDbContext reverseProxyDbContext)
        {
            _reverseProxyDbContext = reverseProxyDbContext;
        }

        public IProxyConfig GetConfig()
        {
            throw new NotImplementedException();
        }
    }
}
