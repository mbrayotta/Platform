using System;
using Xunit;
using Infra.WebHost.Test;
using Newtonsoft.Json;
using Domain;
using System.Net.Http;
using System.Net;

namespace Test
{
    public class ApiModuleTest : IClassFixture<PlatformApiTestFixture>
    {
        readonly PlatformApiTestFixture _platformFixture;
        public ApiModuleTest(PlatformApiTestFixture platformTest) 
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            Environment.SetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES", "Api");
            _platformFixture = platformTest;
        }

        [Fact]
        public void OperationModuleTest()
        {
            var client = _platformFixture.Client;
            var Op = new Operation
            {
                Type = "sumar",
                FirstArgument = 1,
                SecondArgument = 2
            };

            var content = JsonConvert.SerializeObject(Op);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var result = client.PostAsync("/api/operations", byteContent).Result;

            Assert.Equal(HttpStatusCode.Accepted, result.StatusCode);

        }
    }
}
