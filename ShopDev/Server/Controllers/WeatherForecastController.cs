using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopDev.Server.Controllers;
[ApiController]
[Route("[controller]")]
//[Authorize]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "it works...";
    }
}
