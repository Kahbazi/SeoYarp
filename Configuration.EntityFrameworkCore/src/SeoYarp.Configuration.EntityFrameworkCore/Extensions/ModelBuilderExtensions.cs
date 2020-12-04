using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeoYarp.Configuration.EntityFrameworkCore.Entities;
using SeoYarp.Configuration.EntityFrameworkCore.Options;

namespace SeoYarp.Configuration.EntityFrameworkCore.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ConfigureProxyRoutes(this ModelBuilder modelBuilder, ReverseProxyStoreOptions storeOptions)
        {
            if (!string.IsNullOrWhiteSpace(storeOptions.DefaultSchema))
            {
                modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema);
            }

            modelBuilder.Entity<ProxyRoute>(route =>
            {
                route.ToTable(storeOptions.ProxyRoutes);

                route.HasKey(x => x.Id);

                route.Property(x => x.RouteId).HasMaxLength(250).IsRequired();
                route.HasIndex(x => x.RouteId).IsUnique();
                route.OwnsOne(x => x.Match, match =>
                {
                    match.Property(x => x.Methods).HasJsonConversion();
                    match.Property(x => x.Hosts).HasJsonConversion();
                    match.Property(x => x.Path).HasMaxLength(250);
                    match.OwnsMany(x => x.Headers, header =>
                    {
                        header.WithOwner().HasForeignKey("ProxyRouteId");
                        header.Property<int>("Id");
                        header.HasKey("Id");
                        header.Property(x => x.Name).HasMaxLength(250);
                        header.Property(x => x.IsCaseSensitive);
                        header.Property(x => x.Mode);
                        header.Property(x => x.Values).HasJsonConversion();
                    });
                });
                route.Property(x => x.Order);
                route.Property(x => x.ClusterId).HasMaxLength(250);
                route.Property(x => x.AuthorizationPolicy).HasMaxLength(250);
                route.Property(x => x.CorsPolicy).HasMaxLength(250);
                route.Property(x => x.Metadata).HasJsonConversion();
                route.OwnsMany(x => x.Transforms, transform =>
                {
                    transform.WithOwner().HasForeignKey("ProxyRouteId");
                    transform.Property<int>("Id");
                    transform.HasKey("Id");
                    transform.Property(x => x.Data).HasJsonConversion();
                });
            });


            modelBuilder.Entity<Cluster>(cluster =>
            {
                cluster.ToTable(storeOptions.Clusters);

                cluster.HasKey(c => c.Id);

                cluster.Property(c => c.Id)
                    .IsRequired()
                    .ValueGeneratedNever()
                    .HasMaxLength(250);

                cluster.OwnsOne(c => c.LoadBalancing, loadBalancingOptions =>
                {
                    loadBalancingOptions.Property(l => l.Mode);
                });

                cluster.OwnsOne(c => c.SessionAffinity, sessionAffinityOptions =>
                {
                    sessionAffinityOptions.Property(s => s.Enabled);

                    sessionAffinityOptions.Property(s => s.Mode)
                        .HasMaxLength(250)
                        .IsRequired();

                    sessionAffinityOptions.Property(s => s.FailurePolicy)
                        .HasMaxLength(250)
                        .IsRequired();

                    sessionAffinityOptions.Property(s => s.Settings)
                        .HasJsonConversion();
                });

                cluster.OwnsOne(c => c.HealthCheck, healthCheckOptions =>
                {
                    healthCheckOptions.OwnsOne(h => h.Passive, passiveHealthCheckOptions =>
                    {
                        passiveHealthCheckOptions.Property(p => p.Enabled);

                        passiveHealthCheckOptions.Property(p => p.Policy)
                            .HasMaxLength(250);

                        passiveHealthCheckOptions.Property(p => p.ReactivationPeriod);
                    });
                    healthCheckOptions.OwnsOne(h => h.Active, activeHealthCheckOptions =>
                    {
                        activeHealthCheckOptions.Property(p => p.Enabled);
                        activeHealthCheckOptions.Property(p => p.Policy)
                            .HasMaxLength(250);
                        activeHealthCheckOptions.Property(p => p.Path)
                            .HasMaxLength(250);
                        activeHealthCheckOptions.Property(p => p.Interval);
                        activeHealthCheckOptions.Property(p => p.Timeout);
                    });
                });

                cluster.OwnsOne(c => c.HttpRequest, httpRequestOptions =>
                {
                    httpRequestOptions.Property(h => h.RequestTimeout);

                    httpRequestOptions.Property(h => h.Version)
                        .HasDefaultToStringConversion();
                });

                cluster.OwnsMany(c => c.Destinations, destination =>
                {
                    destination.Property(d => d.Address)
                        .HasMaxLength(250)
                        .IsRequired();

                    destination.Property(d => d.Health)
                        .HasMaxLength(250);

                    destination.Property(d => d.Metadata)
                        .HasJsonConversion();
                });

                cluster.Property(x => x.Metadata)
                    .HasJsonConversion();
            });

            return modelBuilder;
        }

        private static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(this PropertyBuilder<TProperty> builder)
        {
            return builder.HasConversion(x => JsonSerializer.Serialize(x, null), x => JsonSerializer.Deserialize<TProperty>(x, null));
        }

        private static PropertyBuilder<TProperty> HasDefaultToStringConversion<TProperty>(this PropertyBuilder<TProperty> builder)
        {
            return builder.HasConversion(x => x.ToString(), x => (TProperty)Activator.CreateInstance(typeof(TProperty), x));
        }

        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration)
            where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(configuration.Schema))
            {
                return entityTypeBuilder.ToTable(configuration.Name);
            }
            else
            {
                return entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
            }
        }
    }
}
