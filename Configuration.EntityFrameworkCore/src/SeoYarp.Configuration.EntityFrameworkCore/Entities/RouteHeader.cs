using System.Collections.Generic;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class RouteHeader
    {
        public string Name { get; set; }
        public IList<string> Values { get; set; }
        public HeaderMatchMode Mode { get; set; }
        public bool IsCaseSensitive { get; set; }
    }
}
