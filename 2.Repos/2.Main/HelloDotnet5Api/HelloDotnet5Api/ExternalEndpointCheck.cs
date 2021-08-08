using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using HelooDotnet5;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace HelloDotnet5
{
    public class ExternalEndpointCheck : IHealthCheck
    {
        private readonly ServiceSettings serviceSettings;
        public ExternalEndpointCheck(IOptions<ServiceSettings> options)
        {
            serviceSettings = options.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(serviceSettings.OpenWeatherHost);
            if (reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}