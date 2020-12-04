using System.Security.Authentication;

namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
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
