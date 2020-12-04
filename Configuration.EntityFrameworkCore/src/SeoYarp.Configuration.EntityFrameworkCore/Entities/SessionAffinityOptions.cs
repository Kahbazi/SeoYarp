using System.Collections.Generic;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class SessionAffinityOptions
    {
        public bool Enabled { get; set; }

        public string Mode { get; set; }

        public string FailurePolicy { get; set; }

        public IDictionary<string, string> Settings { get; set; }
    }
}
