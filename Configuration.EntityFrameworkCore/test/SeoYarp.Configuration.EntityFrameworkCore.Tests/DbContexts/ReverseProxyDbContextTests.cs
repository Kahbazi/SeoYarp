using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SeoYarp.Configuration.EntityFrameworkCore.Entities;
using SeoYarp.Configuration.EntityFrameworkCore.Options;
using SeoYarp.Configuration.EntityFrameworkCore.Tests.IntegrationTests;
using Xunit;

namespace SeoYarp.Configuration.EntityFrameworkCore.Tests.DbContexts
{
    public class ReverseProxyDbContextTests : IntegrationTest<ReverseProxyDbContextTests, ReverseProxyDbContext, ReverseProxyStoreOptions>
    {
        public ReverseProxyDbContextTests(DatabaseProviderFixture<ReverseProxyDbContext> fixture) : base(fixture)
        {
            foreach (var options in TestDatabaseProviders.SelectMany(x => x.Select(y => (DbContextOptions<ReverseProxyDbContext>)y)).ToList())
            {
                using var context = new ReverseProxyDbContext(options, StoreOptions);
                context.Database.EnsureCreated();
            }
        }

        [Theory, MemberData(nameof(TestDatabaseProviders))]
        public void CanAddAndDeleteRouteTransform(DbContextOptions<ReverseProxyDbContext> options)
        {
            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                db.ProxyRoutes.Add(new ProxyRoute
                {
                    RouteId = "test-route-id",
                    ClusterId = "test-cluster-id",
                });

                db.SaveChanges();
            }

            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                // explicit include due to lack of EF Core lazy loading
                var proxyRoute = db.ProxyRoutes.Include(x => x.Transforms).First();

                proxyRoute.Transforms.Add(new Transform
                {
                    Data = new Dictionary<string, string>
                    {
                        ["testKey"] = "testValue"
                    }
                });

                db.SaveChanges();
            }

            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                var proxyRoute = db.ProxyRoutes.Include(x => x.Transforms).First();
                var transform = proxyRoute.Transforms.First();

                proxyRoute.Transforms.Remove(transform);

                db.SaveChanges();
            }

            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                var proxyRoute = db.ProxyRoutes.Include(x => x.Transforms).First();

                Assert.Empty(proxyRoute.Transforms);
            }
        }

        [Theory, MemberData(nameof(TestDatabaseProviders))]
        public void CanAddAndDeleteProxyRoutesMatchHeaders(DbContextOptions<ReverseProxyDbContext> options)
        {
            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                db.ProxyRoutes.Add(new ProxyRoute
                {
                    RouteId = "test-route-id",
                    ClusterId = "test-cluster-id",
                    Match = new ProxyMatch
                    {
                        Methods = new List<string> { "GET" }
                    }
                });

                db.SaveChanges();
            }

            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                var proxyRoute = db.ProxyRoutes.Include(x => x.Match.Headers).First();

                proxyRoute.Match.Headers.Add(new RouteHeader
                {
                    Name = "someHeader"
                });

                db.SaveChanges();
            }

            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                var proxyRoute = db.ProxyRoutes.Include(x => x.Match.Headers).First();
                var header = proxyRoute.Match.Headers.First();

                proxyRoute.Match.Headers.Remove(header);

                db.SaveChanges();
            }

            using (var db = new ReverseProxyDbContext(options, StoreOptions))
            {
                var proxyRoute = db.ProxyRoutes.Include(x => x.Match.Headers).First();

                Assert.Empty(proxyRoute.Match.Headers);
            }
        }
    }
}
