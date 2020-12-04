
namespace SeoYarp.Configuration.EntityFrameworkCore.Entities
{
    public class HealthCheckOptions
    {
        public PassiveHealthCheckOptions Passive { get; set; }

        public ActiveHealthCheckOptions Active { get; set; }
    }
}
