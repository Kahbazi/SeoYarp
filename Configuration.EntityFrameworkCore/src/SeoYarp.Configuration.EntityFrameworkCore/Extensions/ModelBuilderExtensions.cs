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

            return modelBuilder.Entity<ProxyRoute>(route =>
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
        }

        public static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(this PropertyBuilder<TProperty> builder)
        {
            return builder.HasConversion(x => JsonSerializer.Serialize(x, null), x => JsonSerializer.Deserialize<TProperty>(x, null));
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
