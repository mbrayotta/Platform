using System;
using System.Collections.Generic;
using System.Text;
using Carter;
using Persistence.Querys;
using Microsoft.AspNetCore.Http;

namespace Api.Modules
{
    public class OperationResultsModule : CarterModule
    {
        private readonly OperationQuery _operationsQ;
        public OperationResultsModule(OperationQuery operationsQ)
        {
            _operationsQ = operationsQ;

            Get("/api/results/{id}", async (req, res) => {

                string id = req.RouteValues["id"].ToString();
                var op = await _operationsQ.Select(id);

                if(op == null)
                {
                    res.StatusCode = 404;
                }
                else
                {
                    await res.WriteAsync($"Operation \n Id: {op.Id} \n Type: {op.Type} \n FirstArgument: {op.FirstArgument} \n SecondArgument: {op.SecondArgument} \n Total: {op.total}");
                }
            });
        }
    }
}
