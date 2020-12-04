using Microsoft.EntityFrameworkCore;
using SeoYarp.Configuration.EntityFrameworkCore.Entities;

namespace SeoYarp.Configuration.EntityFrameworkCore.Interfaces
{
    public interface IReverseProxyDbContext
    {
        DbSet<ProxyRoute> ProxyRoutes { get; set; }
    }
}
