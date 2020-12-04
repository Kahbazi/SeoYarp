using System.Collections.Generic;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class ProxyMatch 
    {
        public IList<string> Methods { get; set; }
        public IList<string> Hosts { get; set; }
        public string Path { get; set; }
        public IList<RouteHeader> Headers { get; set; }
    }
}
