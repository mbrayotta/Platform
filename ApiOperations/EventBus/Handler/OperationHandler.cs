using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Andreani.Integracion.Eventos.Almacenes;
using Domain;
using Infra.EventBus;
using Persistence.Querys;
using Prometheus;

namespace EventBus.Handler
{
    public class OperationHandler : IIntegrationEventHandler<PedidoCreado>
    {
        private readonly OperationQuery _oPquery;
        private static readonly Gauge _jobsInQueue = Metrics
                .CreateGauge("Operation_Queue", "Number of results received");
        public OperationHandler(OperationQuery oPquery)
        {
            _oPquery = oPquery;
        }
        public async Task Handle(PedidoCreado @event, IConsumeContext context)
        {
            _jobsInQueue.Inc();

            var op = new Operation
            {
                Id = int.Parse(@event.cuentaCorriente),
                FirstArgument = int.Parse(@event.codigoDeContratoInterno),
                SecondArgument = int.Parse(@event.estadoDelPedido),
                Type = @event.cicloDelPedido,
                total = int.Parse(@event.numeroDePedido)
            };

            await _oPquery.Update(op);
        }

        public Task<bool> HandleError(string @event, Exception exception)
        {
            return Task.FromResult(false);
        }
    }
}
