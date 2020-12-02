using System;
using System.Collections.Generic;
using System.Text;

namespace SeoYarp.Configuration.EntityFrameworkCore.Models
{
    public class ProxyHttpRequestOptions
    {
        public TimeSpan? RequestTimeout { get; set; }
        public Version Version { get; set; }
    }
}
