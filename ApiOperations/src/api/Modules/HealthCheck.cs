using System;
using System.Collections.Generic;
using System.Text;
using Carter;
using Carter.Response;
using Infra.HealthCheck.Core.Health;

namespace Api.Modules
{
    public class HealthCheck : CarterModule
    {
        private readonly IHealthIndicator _health;

        public HealthCheck(IHealthIndicator healthIndicator)
        {
            _health = healthIndicator;

            Get("/health/mq", async (req, res) => await res.AsJson(_health.Health()));
        }
    }
}
