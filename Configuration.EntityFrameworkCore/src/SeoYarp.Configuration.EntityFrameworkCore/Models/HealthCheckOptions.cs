
namespace SeoYarp.Configuration.EntityFrameworkCore.Models
{
    public class HealthCheckOptions
    {
        public PassiveHealthCheckOptions Passive { get; set; }

        public ActiveHealthCheckOptions Active { get; set; }
    }
}
