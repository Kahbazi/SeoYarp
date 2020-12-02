using System;
using System.Collections.Generic;

namespace SeoYarp.Configuration.EntityFrameworkCore.Models
{
    public class Cluster
    {
        public string Id { get; set; }
        public LoadBalancingOptions LoadBalancing { get; set; }
        public SessionAffinityOptions SessionAffinity { get; set; }
        public HealthCheckOptions HealthCheck { get; set; }
        public ProxyHttpRequestOptions HttpRequest { get; set; }
        public IList<Destination> Destinations { get; set; }
        public IDictionary<string, string> Metadata { get; set; }

    }
}
