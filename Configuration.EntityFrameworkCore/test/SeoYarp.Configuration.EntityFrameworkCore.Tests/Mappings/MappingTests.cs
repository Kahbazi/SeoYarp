using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text.Json;
using SeoYarp.Configuration.EntityFrameworkCore.Entities;
using SeoYarp.Configuration.EntityFrameworkCore.Mappings;
using Xunit;


namespace SeoYarp.Configuration.EntityFrameworkCore.Tests.Mappings
{
    public class MappingTests
    {
        private readonly IMapper _mapper = new DefaultMapper();

        [Fact]
        public void ClusterTest()
        {
            var destination = new Destination {
                Address = "ADDRESS",
                Health = "HEALTH",
                Key = "KEY",
                Metadata = new Dictionary<string, string> {{"KEY", "VALUE"}},
            };

            var loadBalancingOptions =
                new LoadBalancingOptions {Mode = Microsoft.ReverseProxy.Abstractions.LoadBalancingMode.Random,};

            var activeHealthCheckOptions = new ActiveHealthCheckOptions {
                Enabled = true,
                Interval = TimeSpan.FromSeconds(1),
                Path = "PATH",
                Policy = "POLICY",
                Timeout = TimeSpan.FromSeconds(1)
            };

            var passiveHealthCheckOptions = new PassiveHealthCheckOptions {
                Enabled = true, Policy = "POLICY", ReactivationPeriod = TimeSpan.FromSeconds(1),
            };

            var httpClientOptions = new ProxyHttpClientOptions {SslProtocols = SslProtocols.Tls,};
            var httpRequestOptions =
                new ProxyHttpRequestOptions {Timeout = TimeSpan.FromSeconds(1), Version = new Version(1, 1)};

            var sessionAffinityOptions = new SessionAffinityOptions {
                Enabled = true,
                Mode = "MODE",
                Settings = new Dictionary<string, string> {{"KEY", "VALUE"}},
                FailurePolicy = "FAILURE_POLICY"
            };

            var cluster = new Cluster {
                Id = "DEFAULT",
                Destinations = new List<Destination> {destination},
                Metadata = new Dictionary<string, string> {{"KEY", "VALUE"}},
                HealthCheck =
                    new HealthCheckOptions {Active = activeHealthCheckOptions, Passive = passiveHealthCheckOptions,},
                HttpClient = httpClientOptions,
                HttpRequest = httpRequestOptions,
                LoadBalancing = loadBalancingOptions,
                SessionAffinity = sessionAffinityOptions
            };

            var mappedCluster = _mapper.ToYarpCluster(cluster);
            var (mappedDestinationKey, mappedDestination) = mappedCluster.Destinations.First();
            var mappedLoadBalancingOptions = mappedCluster.LoadBalancing;
            var mappedActiveHealthCheckOptions = mappedCluster.HealthCheck.Active;
            var mappedPassiveHealthCheckOptions = mappedCluster.HealthCheck.Passive;
            var mappedHttpClientOptions = mappedCluster.HttpClient;
            var mappedHttpRequestOptions = mappedCluster.HttpRequest;
            var mappedSessionAffinityOptions = mappedCluster.SessionAffinity;

            // cluster
            Assert.Equal(cluster.Id, mappedCluster.Id);
            Assert.Equal(JsonSerializer.Serialize(cluster.Metadata), JsonSerializer.Serialize(mappedCluster.Metadata));

            // destination
            Assert.Equal(destination.Key, mappedDestinationKey);
            Assert.Equal(destination.Address, mappedDestination.Address);
            Assert.Equal(destination.Health, mappedDestination.Health);
            Assert.Equal(JsonSerializer.Serialize(destination.Metadata),
                JsonSerializer.Serialize(mappedDestination.Metadata));

            // load balancing
            Assert.Equal(mappedLoadBalancingOptions.Mode, loadBalancingOptions.Mode);

            // active health check options
            Assert.Equal(activeHealthCheckOptions.Enabled, mappedActiveHealthCheckOptions.Enabled);
            Assert.Equal(activeHealthCheckOptions.Interval, mappedActiveHealthCheckOptions.Interval);
            Assert.Equal(activeHealthCheckOptions.Path, mappedActiveHealthCheckOptions.Path);
            Assert.Equal(activeHealthCheckOptions.Policy, mappedActiveHealthCheckOptions.Policy);
            Assert.Equal(activeHealthCheckOptions.Timeout, mappedActiveHealthCheckOptions.Timeout);

            // passive health check options
            Assert.Equal(passiveHealthCheckOptions.Enabled, mappedPassiveHealthCheckOptions.Enabled);
            Assert.Equal(passiveHealthCheckOptions.Policy, mappedPassiveHealthCheckOptions.Policy);
            Assert.Equal(passiveHealthCheckOptions.ReactivationPeriod,
                mappedPassiveHealthCheckOptions.ReactivationPeriod);

            // http client options
            Assert.Equal(httpClientOptions.DangerousAcceptAnyServerCertificate,
                mappedHttpClientOptions.DangerousAcceptAnyServerCertificate);
            Assert.Equal(httpClientOptions.SslProtocols, mappedHttpClientOptions.SslProtocols);
            Assert.Equal(httpClientOptions.PropagateActivityContext, mappedHttpClientOptions.PropagateActivityContext);
            Assert.Equal(httpClientOptions.MaxConnectionsPerServer, mappedHttpClientOptions.MaxConnectionsPerServer);

            // http request options
            Assert.Equal(httpRequestOptions.Timeout, mappedHttpRequestOptions.Timeout);
            Assert.Equal(httpRequestOptions.Version.ToString(), mappedHttpRequestOptions.Version.ToString());

            // session affinity options
            Assert.Equal(sessionAffinityOptions.Enabled, mappedSessionAffinityOptions.Enabled);
            Assert.Equal(sessionAffinityOptions.Mode, mappedSessionAffinityOptions.Mode);
            Assert.Equal(sessionAffinityOptions.FailurePolicy, mappedSessionAffinityOptions.FailurePolicy);
            Assert.Equal(JsonSerializer.Serialize(sessionAffinityOptions.Settings),
                JsonSerializer.Serialize(mappedSessionAffinityOptions.Settings));
        }

        [Fact]
        public void RouteTest()
        {
            var routeHeader = new RouteHeader {
                Name = "NAME",
                Mode = HeaderMatchMode.ExactHeader,
                Values = new List<string> {"VALUE"},
                IsCaseSensitive = true
            };
            var proxyMatch = new ProxyMatch {
                Path = "PATH",
                Headers = new List<RouteHeader> {routeHeader},
                Hosts = new List<string> {"HOST"},
                Methods = new List<string> {"METHOD"}
            };
            var transform = new Transform {Data = new Dictionary<string, string> {{"KEY", "VALUE"}}};
            var route = new ProxyRoute {
                Id = int.MaxValue, Match = proxyMatch, Metadata = new Dictionary<string, string> {{"KEY", "VALUE"}},
                Order = 1,
                Transforms = new List<Transform> {transform},
                AuthorizationPolicy = "AUTHORIZATION_POLICY",
                ClusterId = "CLUSTER_ID",
                CorsPolicy = "CORS_POLICY",
                RouteId = "ROUTE_ID",
            };

            var mappedRoute = _mapper.ToYarpProxyRoute(route);
            var mappedProxyMatch = mappedRoute.Match;
            var mappedRouteHeader = mappedRoute.Match.Headers.First();
            var mappedTransform = mappedRoute.Transforms.First();

            // route
            Assert.Equal(route.RouteId, mappedRoute.RouteId);
            Assert.Equal(route.Order, mappedRoute.Order);
            Assert.Equal(route.AuthorizationPolicy, mappedRoute.AuthorizationPolicy);
            Assert.Equal(route.ClusterId, mappedRoute.ClusterId);
            Assert.Equal(route.CorsPolicy, mappedRoute.CorsPolicy);
            Assert.Equal(route.RouteId, mappedRoute.RouteId);

            // route match
            Assert.Equal(proxyMatch.Path, mappedProxyMatch.Path);
            Assert.Equal(JsonSerializer.Serialize(proxyMatch.Methods), JsonSerializer.Serialize(mappedProxyMatch.Methods));
            Assert.Equal(JsonSerializer.Serialize(proxyMatch.Hosts), JsonSerializer.Serialize(mappedProxyMatch.Hosts));

            // route header
            Assert.Equal(routeHeader.Mode, (HeaderMatchMode)mappedRouteHeader.Mode);
            Assert.Equal(routeHeader.Name, mappedRouteHeader.Name);
            Assert.Equal(routeHeader.IsCaseSensitive, mappedRouteHeader.IsCaseSensitive);
            Assert.Equal(JsonSerializer.Serialize(routeHeader.Values), JsonSerializer.Serialize(mappedRouteHeader.Values));

            // transform
            Assert.Equal(JsonSerializer.Serialize(transform.Data), JsonSerializer.Serialize(mappedTransform));
        }
    }
}
