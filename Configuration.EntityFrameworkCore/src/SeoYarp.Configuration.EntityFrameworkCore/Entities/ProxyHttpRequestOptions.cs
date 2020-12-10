using System;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class ProxyHttpRequestOptions
    {
        public TimeSpan? RequestTimeout { get; set; }
        public Version Version { get; set; }
    }
}
