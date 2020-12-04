using Microsoft.EntityFrameworkCore;
using SeoYarp.Configuration.EntityFrameworkCore.Entities;
using SeoYarp.Configuration.EntityFrameworkCore.Extensions;
using SeoYarp.Configuration.EntityFrameworkCore.Interfaces;
using SeoYarp.Configuration.EntityFrameworkCore.Options;

namespace SeoYarp.Configuration.EntityFrameworkCore
{
    public class ReverseProxyDbContext : ReverseProxyDbContext<ReverseProxyDbContext>
    {
        public ReverseProxyDbContext(DbContextOptions<ReverseProxyDbContext> options, ReverseProxyStoreOptions storeOptions)
            : base(options, storeOptions)
        {
        }
    }

    public class ReverseProxyDbContext<TContext> : DbContext, IReverseProxyDbContext
        where TContext : DbContext
    {
        private readonly ReverseProxyStoreOptions _storeOptions;

        public ReverseProxyDbContext(DbContextOptions<ReverseProxyDbContext> options, ReverseProxyStoreOptions storeOptions)
            : base(options)
        {
            _storeOptions = storeOptions;
        }

        public DbSet<ProxyRoute> ProxyRoutes { get; set; }
        public DbSet<Cluster> Clusters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureProxyRoutes(_storeOptions);
        }
    }
}
