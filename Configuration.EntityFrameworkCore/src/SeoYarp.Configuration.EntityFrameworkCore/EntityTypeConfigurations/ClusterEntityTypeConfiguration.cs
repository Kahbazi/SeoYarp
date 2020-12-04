using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeoYarp.Configuration.EntityFrameworkCore.Entities;
using SeoYarp.Configuration.EntityFrameworkCore.Options;
using SeoYarp.Configuration.EntityFrameworkCore.Extensions;

namespace SeoYarp.Configuration.EntityFrameworkCore.EntityTypeConfigurations
{
    public class ClusterEntityTypeConfiguration: IEntityTypeConfiguration<Cluster>
    {
        private readonly ReverseProxyStoreOptions _storeOptions;

        public ClusterEntityTypeConfiguration(ReverseProxyStoreOptions storeOptions)
        {
            _storeOptions = storeOptions;
        }

        public void Configure(EntityTypeBuilder<Cluster> builder)
        {



        }
    }
}
