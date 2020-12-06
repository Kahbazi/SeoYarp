using System.Collections.Generic;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class Destination
    {
        public string Key { get; set; }
        public string Address { get; set; }
        public string Health { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}
