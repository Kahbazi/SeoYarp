using ActiveHealthCheckOptions = Microsoft.ReverseProxy.Abstractions.ActiveHealthCheckOptions;
using Cluster = Microsoft.ReverseProxy.Abstractions.Cluster;
using Destination = Microsoft.ReverseProxy.Abstractions.Destination;
using HealthCheckOptions = Microsoft.ReverseProxy.Abstractions.HealthCheckOptions;
using LoadBalancingOptions = Microsoft.ReverseProxy.Abstractions.LoadBalancingOptions;
using PassiveHealthCheckOptions = Microsoft.ReverseProxy.Abstractions.PassiveHealthCheckOptions;
using ProxyHttpClientOptions = Microsoft.ReverseProxy.Abstractions.ProxyHttpClientOptions;
using ProxyHttpRequestOptions = Microsoft.ReverseProxy.Abstractions.ProxyHttpRequestOptions;
using ProxyMatch = Microsoft.ReverseProxy.Abstractions.ProxyMatch;
using ProxyRoute = Microsoft.ReverseProxy.Abstractions.ProxyRoute;
using RouteHeader = Microsoft.ReverseProxy.Abstractions.RouteHeader;
using SessionAffinityOptions = Microsoft.ReverseProxy.Abstractions.SessionAffinityOptions;

namespace SeoYarp.Configuration.EntityFrameworkCore.Mappings
{
    public interface IMapper
    {
        ActiveHealthCheckOptions ToYarpActiveHealthCheckOptions(Entities.ActiveHealthCheckOptions activeHealthCheckOptions);
        Cluster ToYarpCluster(Entities.Cluster cluster);
        Destination ToYarpDestination(Entities.Destination destination);
        HealthCheckOptions ToYarpHealthCheckOptions(Entities.HealthCheckOptions healthCheckOptions);
        LoadBalancingOptions ToYarpLoadBalancingOptions(Entities.LoadBalancingOptions loadBalancingOptions);
        PassiveHealthCheckOptions ToYarpPassiveHealthCheckOptions(Entities.PassiveHealthCheckOptions passiveHealthCheckOptions);
        ProxyHttpClientOptions ToYarpProxyHttpClientOptions(Entities.ProxyHttpClientOptions httpClientOptions);
        ProxyHttpRequestOptions ToYarpProxyHttpRequestOptions(Entities.ProxyHttpRequestOptions httpRequestOptions);
        ProxyMatch ToYarpProxyMatch(Entities.ProxyMatch proxyMatch);
        ProxyRoute ToYarpProxyRoute(Entities.ProxyRoute proxyRoute);
        RouteHeader ToYarpRouteHeader(Entities.RouteHeader routeHeader);
        SessionAffinityOptions ToYarpSessionAffinityOptions(Entities.SessionAffinityOptions sessionAffinityOptions);
    }
}
