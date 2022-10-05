using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Bookstore.HealthChecks
{
    public class MyCustomHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public MyCustomHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            {
                try
                {
                    
                }
                catch (SqlException e)
                {
                    return HealthCheckResult.Unhealthy(e.Message);
                }
            }
            return HealthCheckResult.Healthy("My Custom Health Check OK");
        }
    }
}
