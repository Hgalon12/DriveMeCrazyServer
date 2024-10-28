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
    [HttpPost("login")]
    public IActionResult Login([FromBody] DriveMeCrazyServer.DTO.LoginDto loginDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Get model user class from DB with matching email. 
            DriveMeCrazyServer.Models.TableUser? modelsUser = context.GetUser(loginDto.UserEmail);

            //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
            if (modelsUser == null || modelsUser.UserPassword != loginDto.UserPassword)
            {
                return Unauthorized();
            }

            //Login suceed! now mark login in session memory!
            HttpContext.Session.SetString("loggedInUser", modelsUser.UserEmail);

            DriveMeCrazyServer.DTO.TableUserDto dtoUser = new DriveMeCrazyServer.DTO.TableUserDto(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}

