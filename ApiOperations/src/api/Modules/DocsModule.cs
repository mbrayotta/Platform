using Carter;
using Microsoft.AspNetCore.Http;
using Carter.Response;

namespace PlatformAndreani.Modules
{
    public class DoscModule : CarterModule
    {
        public DoscModule()
        {
            Get("openapi.yaml", async (req, res) => await res.AsFile("openapi.yaml"));
        }
    }
}