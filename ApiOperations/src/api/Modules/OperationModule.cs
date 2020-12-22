using System;
using Carter;
using Domain;
using Carter.ModelBinding;
using Microsoft.AspNetCore.Http;
using System.Net;
using Persistence.Querys;
using Domain.Validators;
using Infra.Web.Problems;
using EventBus.Publisher;
using Prometheus;

namespace Api.Modules
{
    public class OperationModule : CarterModule
    {
        private OperationQuery _operationsQ;
        private OperationValidator _opValidator;
        private PublishOperation _publishOpt;
        private static readonly Gauge _jobsInQueue = Metrics
                .CreateGauge("Operation_Queue", "Number of operations to resolve");
        public OperationModule(OperationQuery operationsQ, OperationValidator opValidator, PublishOperation p_opt)
        {
            _operationsQ = operationsQ;
            _opValidator = opValidator;
            _publishOpt = p_opt;

            Post("/api/operations", async (req, res) =>
            {
                var data = req.Bind<Operation>();
                _jobsInQueue.Inc();

                Console.WriteLine($"type : {data.Result.Type}, a : {data.Result.FirstArgument}, b : {data.Result.SecondArgument}");

                var operation = new Operation
                {
                    Type = data.Result.Type,
                    FirstArgument = data.Result.FirstArgument,
                    SecondArgument = data.Result.SecondArgument
                };

                var validationResult = await _opValidator.ValidateAsync(operation);
                
                if (validationResult.IsValid)
                {
                    var id = await _operationsQ.Insert(operation);
                    
                    if (!string.IsNullOrEmpty(id.ToString()))
                    {
                        operation.Id = id;
                        _publishOpt.PublishOp(operation);
                        
                        res.StatusCode = 202;
                        res.Headers.Add("LocationUrl", $"/api/results/{id}");
                    }
                    await res.WriteAsync($"Id : {id}");
                }
                else 
                { 
                    await res.AsProblem(validationResult);
                }
                
                
            }
            );
        }
    }
}
