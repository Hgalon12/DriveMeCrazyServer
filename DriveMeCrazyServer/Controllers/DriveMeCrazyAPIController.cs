﻿using DriveMeCrazyServer.Models;
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

    [HttpPost("register")]
    public IActionResult Register([FromBody] DriveMeCrazyServer.DTO.TableUserDto userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            DriveMeCrazyServer.Models.TableUser modelsUser = userDto.GetModel();

            context.TableUsers.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            DriveMeCrazyServer.DTO.TableUserDto dtoUser = new DriveMeCrazyServer.DTO.TableUserDto(modelsUser);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpPost("registerCar")]
    public IActionResult RegisterCar([FromBody] DriveMeCrazyServer.DTO.TableCarDto carDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            DriveMeCrazyServer.Models.TableCar modelsUser = carDto.GetModel();

            context.TableCars.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            DriveMeCrazyServer.DTO.TableCarDto dtoUser = new DriveMeCrazyServer.DTO.TableCarDto(modelsUser);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.IdCar);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }



    [HttpPost("UploadProfileImage")]
    public async Task<IActionResult> UploadProfileImageAsync(IFormFile file)
    {
        //Check if who is logged in
        string? userEmail = HttpContext.Session.GetString("loggedInUser");
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User is not logged in");
        }

        //Get model user class from DB with matching email. 
        DriveMeCrazyServer.Models.TableUser? user = context.GetUser(userEmail);
        //Clear the tracking of all objects to avoid double tracking
        context.ChangeTracker.Clear();

        if (user == null)
        {
            return Unauthorized("User is not found in the database");
        }


        //Read all files sent
        long imagesSize = 0;

        if (file.Length > 0)
        {
            //Check the file extention!
            string[] allowedExtentions = { ".png", ".jpg" };
            string extention = "";
            if (file.FileName.LastIndexOf(".") > 0)
            {
                extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
            }
            if (!allowedExtentions.Where(e => e == extention).Any())
            {
                //Extention is not supported
                return BadRequest("File sent with non supported extention");
            }

            //Build path in the web root (better to a specific folder under the web root
            string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{user.Id}{extention}";

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);

                if (IsImage(stream))
                {
                    imagesSize += stream.Length;
                }
                else
                {
                    //Delete the file if it is not supported!
                    System.IO.File.Delete(filePath);
                }

            }

        }

        DriveMeCrazyServer.DTO.TableUserDto dtoUser = new DriveMeCrazyServer.DTO.TableUserDto(user);
        dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
        return Ok(dtoUser);
    }

    //Helper functions

    //this function gets a file stream and check if it is an image
    private static bool IsImage(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);

        List<string> jpg = new List<string> { "FF", "D8" };
        List<string> bmp = new List<string> { "42", "4D" };
        List<string> gif = new List<string> { "47", "49", "46" };
        List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
        List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

        List<string> bytesIterated = new List<string>();

        for (int i = 0; i < 8; i++)
        {
            string bit = stream.ReadByte().ToString("X2");
            bytesIterated.Add(bit);

            bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
            if (isImage)
            {
                return true;
            }
        }

        return false;
    }














    private string GetProfileImageVirtualPath(int userId)
    {
        string virtualPath = $"/profileImages/{userId}";
        string path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.png";
        if (System.IO.File.Exists(path))
        {
            virtualPath += ".png";
        }
        else
        {
            path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.jpg";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".jpg";
            }
            else
            {
                virtualPath = $"/profileImages/default.png";
            }
        }

        return virtualPath;
    }




}

