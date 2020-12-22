using Andreani.Integracion.Eventos.Almacenes;
using EventBus.Handler;
using EventBus.Publisher;
using Infra.Full.Hosting;
using MathematicalOperations.BasicMathematicalOperations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CalculatorService
{
  public class Startup : IStartup
  {
    public void Configure(HostBuilderContext context, IServiceCollection services)
    {
            services.AddTransient<Sum>();
            services.AddTransient<PublishResult>();
            services.AddIbmMQEventBus(context.Configuration)
                    .Subscribe<PedidoAsignado, OperationHandler>();
    }
    public void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
    {
    }
  }
}
