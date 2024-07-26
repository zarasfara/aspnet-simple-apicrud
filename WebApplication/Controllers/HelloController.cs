using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers;

[ApiController]
public class HelloController : ControllerBase
{
    [HttpGet("[controller]/hello")]
    public IActionResult Hello()
    {
        return Ok("hello, world!");
    }
}