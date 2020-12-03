using System;
using System.Collections.Generic;
using System.Text;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class ProxyRoute
    {
        public int Id { get; set; }

        public string RouteId { get; set; }
        public ProxyMatch Match { get; set; }
        public int? Order { get; set; }
        public string ClusterId { get; set; }
        public string AuthorizationPolicy { get; set; }
        public string CorsPolicy { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
        public IList<Transform> Transforms { get; set; }
    }
}
