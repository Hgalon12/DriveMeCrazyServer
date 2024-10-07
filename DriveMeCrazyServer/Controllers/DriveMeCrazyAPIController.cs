using DriveMeCrazyServer.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api")]
[ApiController]
public class DriveMeCrazyAPIController : ControllerBase
{
    //a variable to hold a reference to the db context!
    private DriveMeCrazyDbContext context;
    //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
    private IWebHostEnvironment webHostEnvironment;
    //Use dependency injection to get the db context and web host into the constructor
    public DriveMeCrazyAPIController(DriveMeCrazyDbContext context, IWebHostEnvironment env)
    {
        this.context = context;
        this.webHostEnvironment = env;
    }

    [HttpGet]
    [Route("TestServer")]
    public ActionResult<string> TestServer()
    {
        return Ok("Server Responded Successfully");
    }

}

