using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ReverseProxy.Abstractions;

namespace SeoYarp.Configuration.EntityFrameworkCore.Mappings
{
    /// <summary>
    /// This class will provide extension methods for converting entities to yarp classes
    /// </summary>
    public class DefaultMapper : IMapper
    {
        public ActiveHealthCheckOptions ToYarpActiveHealthCheckOptions(
            Entities.ActiveHealthCheckOptions activeHealthCheckOptions)
        {
            _ = activeHealthCheckOptions ?? throw new ArgumentNullException(nameof(activeHealthCheckOptions));

            return new ActiveHealthCheckOptions {
                Enabled = activeHealthCheckOptions.Enabled,
                Interval = activeHealthCheckOptions.Interval,
                Timeout = activeHealthCheckOptions.Timeout,
                Path = activeHealthCheckOptions.Path,
                Policy = activeHealthCheckOptions.Policy,
            };
        }

        public Cluster ToYarpCluster(Entities.Cluster cluster)
        {
            _ = cluster ?? throw new ArgumentNullException(nameof(cluster));

            var newCluster = new Cluster {
                Id = cluster.ClusterId,
                Metadata = cluster.Metadata,
                HealthCheck = ToYarpHealthCheckOptions(cluster.HealthCheck),
                HttpClient = ToYarpProxyHttpClientOptions(cluster.HttpClient),
                HttpRequest = ToYarpProxyHttpRequestOptions(cluster.HttpRequest),
                LoadBalancing = ToYarpLoadBalancingOptions(cluster.LoadBalancing),
                SessionAffinity = ToYarpSessionAffinityOptions(cluster.SessionAffinity)
            };

            foreach (var destination in cluster.Destinations)
            {
                newCluster.Destinations.Add(destination.Key, ToYarpDestination(destination));
            }

            return newCluster;
        }

        public Destination ToYarpDestination(Entities.Destination destination)
        {
            _ = destination ?? throw new ArgumentNullException(nameof(destination));

            return new Destination {
                Address = destination.Address, Health = destination.Health, Metadata = destination.Metadata
            };
        }

        public HealthCheckOptions ToYarpHealthCheckOptions(Entities.HealthCheckOptions healthCheckOptions)
        {
            _ = healthCheckOptions ?? throw new ArgumentNullException(nameof(healthCheckOptions));

            return new HealthCheckOptions {
                Active = ToYarpActiveHealthCheckOptions(healthCheckOptions.Active),
                Passive = ToYarpPassiveHealthCheckOptions(healthCheckOptions.Passive),
            };
        }

        public LoadBalancingOptions ToYarpLoadBalancingOptions(Entities.LoadBalancingOptions loadBalancingOptions)
        {
            _ = loadBalancingOptions ?? throw new ArgumentNullException(nameof(loadBalancingOptions));

            return new LoadBalancingOptions {Mode = loadBalancingOptions.Mode};
        }

        public PassiveHealthCheckOptions ToYarpPassiveHealthCheckOptions(
            Entities.PassiveHealthCheckOptions passiveHealthCheckOptions)
        {
            _ = passiveHealthCheckOptions ?? throw new ArgumentNullException(nameof(passiveHealthCheckOptions));

            return new PassiveHealthCheckOptions {
                Enabled = passiveHealthCheckOptions.Enabled,
                Policy = passiveHealthCheckOptions.Policy,
                ReactivationPeriod = passiveHealthCheckOptions.ReactivationPeriod
            };
        }

        public ProxyHttpClientOptions ToYarpProxyHttpClientOptions(Entities.ProxyHttpClientOptions httpClientOptions)
        {
            _ = httpClientOptions ?? throw new ArgumentNullException(nameof(httpClientOptions));

            return new ProxyHttpClientOptions {
                // todo: develop a certificate provider
                ClientCertificate = null,
                SslProtocols = httpClientOptions.SslProtocols,
                PropagateActivityContext = httpClientOptions.PropagateActivityContext,
                MaxConnectionsPerServer = httpClientOptions.MaxConnectionsPerServer,
                DangerousAcceptAnyServerCertificate = httpClientOptions.DangerousAcceptAnyServerCertificate
            };
        }

        public ProxyHttpRequestOptions ToYarpProxyHttpRequestOptions(
            Entities.ProxyHttpRequestOptions httpRequestOptions)
        {
            _ = httpRequestOptions ?? throw new ArgumentNullException(nameof(httpRequestOptions));

            return new ProxyHttpRequestOptions {
                Timeout = httpRequestOptions.Timeout, Version = httpRequestOptions.Version,
            };
        }

        public ProxyMatch ToYarpProxyMatch(Entities.ProxyMatch proxyMatch)
        {
            _ = proxyMatch ?? throw new ArgumentNullException(nameof(proxyMatch));

            return new ProxyMatch {
                Headers = proxyMatch.Headers?
                    .Select(h => ToYarpRouteHeader(h))
                    .ToImmutableList(),
                Hosts = proxyMatch.Hosts?
                    .ToImmutableList(),
                Methods = proxyMatch.Methods?
                    .ToImmutableList(),
                Path = proxyMatch.Path
            };
        }

        public ProxyRoute ToYarpProxyRoute(Entities.ProxyRoute proxyRoute)
        {
            _ = proxyRoute ?? throw new ArgumentNullException(nameof(proxyRoute));

            var route = new ProxyRoute {
                Metadata = proxyRoute.Metadata,
                Order = proxyRoute.Order,
                Transforms = proxyRoute.Transforms?
                    .Select(t => t.Data)
                    .ToImmutableList(),
                AuthorizationPolicy = proxyRoute.AuthorizationPolicy,
                ClusterId = proxyRoute.ClusterId,
                CorsPolicy = proxyRoute.CorsPolicy,
                RouteId = proxyRoute.RouteId,
            };

            route.Match.Headers = proxyRoute.Match?.Headers?
                .Select(ToYarpRouteHeader)
                .ToImmutableList();

            route.Match.Hosts = proxyRoute.Match?.Hosts?
                .ToImmutableList();

            route.Match.Methods = proxyRoute.Match?.Methods?
                .ToImmutableList();

            route.Match.Path = proxyRoute.Match?.Path;

            return route;
        }

        public RouteHeader ToYarpRouteHeader(Entities.RouteHeader routeHeader)
        {
            _ = routeHeader ?? throw new ArgumentNullException(nameof(routeHeader));

            return new RouteHeader {
                Mode = (HeaderMatchMode)routeHeader.Mode,
                Name = routeHeader.Name,
                Values = routeHeader.Values?.ToImmutableList(),
                IsCaseSensitive = routeHeader.IsCaseSensitive
            };
        }

        public SessionAffinityOptions ToYarpSessionAffinityOptions(
            Entities.SessionAffinityOptions sessionAffinityOptions)
        {
            _ = sessionAffinityOptions ?? throw new ArgumentNullException(nameof(sessionAffinityOptions));

            return new SessionAffinityOptions {
                Enabled = sessionAffinityOptions.Enabled,
                Mode = sessionAffinityOptions.Mode,
                Settings = sessionAffinityOptions.Settings,
                FailurePolicy = sessionAffinityOptions.FailurePolicy
            };
        }
    }
}
