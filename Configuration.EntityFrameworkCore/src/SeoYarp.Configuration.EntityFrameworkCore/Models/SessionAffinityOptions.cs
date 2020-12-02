using System;
using System.Collections.Generic;
using System.Text;

namespace SeoYarp.Configuration.EntityFrameworkCore.Models
{
    public class SessionAffinityOptions
    {
        public bool Enabled { get; set; }

        public string Mode { get; set; }

        public string FailurePolicy { get; set; }

        public IDictionary<string, string> Settings { get; set; }
    }
}
