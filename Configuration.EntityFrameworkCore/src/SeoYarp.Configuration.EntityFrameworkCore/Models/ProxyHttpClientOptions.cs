using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SeoYarp.Configuration.EntityFrameworkCore.Models
{
    public class ProxyHttpClientOptions
    {
        public SslProtocols? SslProtocols { get; set; }

        public bool DangerousAcceptAnyServerCertificate { get; set; }

        //public X509Certificate2 ClientCertificate { get; set; }

        public int? MaxConnectionsPerServer { get; set; }

        public bool? PropagateActivityContext { get; set; }
    }
}
