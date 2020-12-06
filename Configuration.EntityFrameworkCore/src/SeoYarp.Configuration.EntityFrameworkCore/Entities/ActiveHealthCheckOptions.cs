using System;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class ActiveHealthCheckOptions
    {
        public bool Enabled { get; set; }
        public TimeSpan? Interval { get; set; }
        public TimeSpan? Timeout { get; set; }
        public string Policy { get; set; }
        public string Path { get; set; }



    }
}
