using System.Collections.Generic;

namespace SeoYarp.Configuration.EntityFrameworkCore.Models
{
    public class Destination
    {
        public string ClusterId { get; set; }
        public string Address { get; set; }

        public string Health { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}
