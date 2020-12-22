using System;
using System.Collections.Generic;
using System.Text;
using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Domain;
using Infra.EventBus;

namespace EventBus.Publisher
{
    public class PublishOperation
    {
        private readonly IEventBus _eventBus;

        public PublishOperation(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishOp(Operation opt) 
        {
            var myEvent = new ConstruirEvento<PedidoAsignado>()
                        .DesdeLaApp("REMITENTE")
                        .ConDestino("QL.BORRARME.REQ")
                        .Crear(); 

            myEvent.cicloDelPedido = opt.Type;
            myEvent.codigoDeContratoInterno = opt.FirstArgument.ToString();
            myEvent.estadoDelPedido = opt.SecondArgument.ToString();
            myEvent.cuentaCorriente = opt.Id.ToString();
            myEvent.numeroDePedido = string.Empty;
            myEvent.cuando = string.Empty;



            _eventBus.Publish(myEvent);
        }
    }
}
