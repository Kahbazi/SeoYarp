using System;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class PassiveHealthCheckOptions
    {
        public bool Enabled { get; set; }
        public string Policy { get; set; }
        public TimeSpan? ReactivationPeriod { get; set; }
    }
}
