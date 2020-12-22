using Carter;
using Microsoft.AspNetCore.Http;
using Carter.Response;

namespace PlatformAndreani.Modules
{
  public class HomeModule : CarterModule
  {
    public HomeModule()
    {
      Get("/", async (req, res) => await res.WriteAsync("welcome to Operation Api") );
    }
  }
}
