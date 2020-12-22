using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Andreani.Integracion.Eventos.Almacenes;
using EventBus.Publisher;
using Infra.EventBus;
using MathematicalOperations.BasicMathematicalOperations;

namespace EventBus.Handler
{
    public class OperationHandler : IIntegrationEventHandler<PedidoAsignado>
    {
        private Sum _sum;
        private readonly PublishResult _publish;

        public OperationHandler(Sum sum, PublishResult publishR)
        {
            _sum = sum;
            _publish = publishR;
        }
        public Task Handle(PedidoAsignado @event, IConsumeContext context)
        {
            var dictionaryOP = new Dictionary<string, string>();
            var result = _sum.SumOperation(Int32.Parse(@event.codigoDeContratoInterno), Int32.Parse(@event.estadoDelPedido));

            dictionaryOP["id"] = @event.cuentaCorriente;
            dictionaryOP["firstArgument"] = @event.codigoDeContratoInterno;
            dictionaryOP["SecondArgument"] = @event.estadoDelPedido;
            dictionaryOP["type"] = @event.cicloDelPedido;
            dictionaryOP["total"] = result.ToString();

            _publish.publishResultOperation(dictionaryOP);
            
            return Task.FromResult(0);
        }

        public Task<bool> HandleError(string @event, Exception exception)
        {
            return Task.FromResult(false);
        }
    }
}
