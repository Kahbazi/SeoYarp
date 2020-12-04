using System;
using Microsoft.EntityFrameworkCore;

namespace SeoYarp.Configuration.EntityFrameworkCore.Options
{
    /// <summary>
    /// Options for configuring the operational context.
    /// </summary>
    public class ReverseProxyStoreOptions
    {
        /// <summary>
        /// Callback to configure the EF DbContext.
        /// </summary>
        /// <value>
        /// The configure database context.
        /// </value>
        public Action<DbContextOptionsBuilder> ConfigureDbContext { get; set; }

        /// <summary>
        /// Callback in DI resolve the EF DbContextOptions. If set, ConfigureDbContext will not be used.
        /// </summary>
        /// <value>
        /// The configure database context.
        /// </value>
        public Action<IServiceProvider, DbContextOptionsBuilder> ResolveDbContextOptions { get; set; }

        /// <summary>
        /// Gets or sets the default schema.
        /// </summary>
        /// <value>
        /// The default schema.
        /// </value>
        public string DefaultSchema { get; set; } = null;

        /// <summary>
        /// Gets or sets the proxy routes table configuration.
        /// </summary>
        /// <value>
        /// The proxy routes.
        /// </value>
        public TableConfiguration ProxyRoutes { get; set; } = new TableConfiguration("ProxyRoutes");

        /// <summary>
        /// Gets or sets the route headers table configuration.
        /// </summary>
        /// <value>
        /// The route headers.
        /// </value>
        public TableConfiguration RouteHeaders { get; set; } = new TableConfiguration("RouteHeaders");

        /// <summary>
        /// Gets or sets the route transforms table configuration.
        /// </summary>
        /// <value>
        /// The route transforms.
        /// </value>
        public TableConfiguration RouteTransforms { get; set; } = new TableConfiguration("RouteTransforms");

        /// <summary>
        /// Gets or sets the clusters table configuration.
        /// </summary>
        /// <value>
        /// The clusters.
        /// </value>
        public TableConfiguration Clusters { get; set; } = new TableConfiguration("Clusters");
    }
}
