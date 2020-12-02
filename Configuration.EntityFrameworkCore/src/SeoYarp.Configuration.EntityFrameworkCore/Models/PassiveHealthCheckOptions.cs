using System;
using System.Collections.Generic;
using System.Text;

namespace SeoYarp.Configuration.EntityFrameworkCore.Models
{
    public class PassiveHealthCheckOptions
    {
        public bool Enabled { get; set; }
        public string Policy { get; set; }
        public TimeSpan? ReactivationPeriod { get; set; }
    }
}
