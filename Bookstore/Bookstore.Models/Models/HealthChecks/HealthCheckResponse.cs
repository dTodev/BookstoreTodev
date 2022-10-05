using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Models.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; init; }
        public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; init; }
        public TimeSpan HealthCheckDuration { get; init; }
    }
}
