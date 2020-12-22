using System;
using System.Collections.Generic;
using System.Text;
using Andreani.Integracion.Esquemas.Eventos;
using Andreani.Integracion.Eventos.Almacenes;
using Infra.EventBus;

namespace EventBus.Publisher
{
    public class PublishResult
    {
        private readonly IEventBus _eventBus;
        
        public PublishResult(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void publishResultOperation(Dictionary<string,string> dictionaryOp)
        {
            var myEvent = new ConstruirEvento<PedidoCreado>()
                        .DesdeLaApp("REMITENTE")
                        .ConDestino("QL.BULPYMED.TEST.REQ")
                        .Crear();

            myEvent.numeroDePedido = dictionaryOp["total"];
            myEvent.cuentaCorriente = dictionaryOp["id"];
            myEvent.cicloDelPedido = dictionaryOp["type"];
            myEvent.codigoDeContratoInterno = dictionaryOp["firstArgument"];
            myEvent.estadoDelPedido = dictionaryOp["SecondArgument"];
            myEvent.cuando = string.Empty;

            _eventBus.Publish(myEvent);
        }
    }
}
