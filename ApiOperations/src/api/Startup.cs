using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Infra.Data.DependencyInjection;
using Persistence.DataAccess;
using Persistence.Querys;
using FluentValidation.AspNetCore;
using Domain.Validators;
using Infra.EventBus.IbmMQ;
using EventBus.Publisher;
using Andreani.Integracion.Eventos.Almacenes;
using EventBus.Handler;
using Infra.Metrics;
using Infra.HealthCheck.IbmMQ;
using Infra.HealthCheck.Core.Health;

[assembly: HostingStartup(typeof(PlatformAndreani.Startup))]

namespace PlatformAndreani
{
  public class Startup : IHostingStartup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    public void Configure(IWebHostBuilder builder)
    {
      builder.ConfigureServices((ctx, c) =>
      {
          c.AddMetrics();
          c.AddDataAccessRegistry();
          c.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OperationValidator>());
          c.AddIbmMQEventBus(ctx.Configuration)
            .Subscribe<PedidoCreado, OperationHandler>();
          c.AddSingleton<IHealthIndicator, IbmQueueHealthIndicator>(s =>
                new IbmQueueHealthIndicator(ctx.Configuration["IbmMQ:QueueManagerName"],
                    ctx.Configuration["IbmMQ:ConnectionInfo"],
                    ctx.Configuration["IbmMQ:Channel"],
                    ctx.Configuration["IbmMQ:QueueName"]));


          c.AddTransient<DataAccessOperation>();
          c.AddTransient<OperationQuery>();
          c.AddTransient<PublishOperation>();

      });
    }
  }
}
