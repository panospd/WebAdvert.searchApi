using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Nest;

namespace WebAdvert.SearchApi.HealthChecks
{
    public class ElasticClientHealthCheck : IHealthCheck
    {
        private readonly IElasticClient _elasticClient;

        public ElasticClientHealthCheck(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var healthDocument = await _elasticClient.CatHealthAsync(cancellationToken: cancellationToken);

            var response = healthDocument.Records.FirstOrDefault(c => c.Cluster.Contains("advertapies"));

            if(response.Status == "red")
                return HealthCheckResult.Unhealthy();

            return HealthCheckResult.Healthy();
        }
    }
}
