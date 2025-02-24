using DriveMeCrazyServer.DTO;
using DriveMeCrazyServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;


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

            //CHeck if the user if registered as a kid and not a parent
            if (userDto.CarOwnerEmail != null)
            {
                //Check what is the ID of the Parent
                TableUser? parent = context.GetUser(userDto.CarOwnerEmail);
                if (parent == null)
                {
                    return BadRequest("Parent email not found!");
                }
                modelsUser.CarOwnerId = parent.Id;
            }

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

            //Create model car class
            DriveMeCrazyServer.Models.TableCar modelsCar = carDto.GetModel();

            context.TableCars.Add(modelsCar);
            context.SaveChanges();

            //Car was added!
            DriveMeCrazyServer.DTO.TableCarDto dtoCar = new DriveMeCrazyServer.DTO.TableCarDto(modelsCar);
            dtoCar.CarImagePath = GetCarImageVirtualPath(dtoCar.IdCar);
            return Ok(dtoCar);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("UploadCarImage")]
    public async Task<IActionResult> UploadCarImageAsync(IFormFile file, [FromQuery] string carId)
    {
        //Check if who is logged in
        //string? userEmail = HttpContext.Session.GetString("loggedInUser");
        //if (string.IsNullOrEmpty(userEmail))
        //{
        //    return Unauthorized("User is not logged in");
        //}

        TableCar? car = context.TableCars.Where(c => c.IdCar == carId).FirstOrDefault();
        if (car == null)
        {
            return BadRequest();
        }

        string path = $"\\carImages\\{carId}";

        HttpStatusCode result =  await UploadImageAsync(file, path);

        if (result != HttpStatusCode.OK)
            return BadRequest();

        DriveMeCrazyServer.DTO.TableCarDto dtoCar = new DriveMeCrazyServer.DTO.TableCarDto(car);
        dtoCar.CarImagePath = GetCarImageVirtualPath(dtoCar.IdCar);
        return Ok(dtoCar);

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

        string path = $"\\profileImages\\{user.Id}";
        HttpStatusCode result =  await UploadImageAsync(file, path);

        if (result != HttpStatusCode.OK)
            return BadRequest();

        DriveMeCrazyServer.DTO.TableUserDto dtoUser = new DriveMeCrazyServer.DTO.TableUserDto(user);
        dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.Id);
        return Ok(dtoUser);

    }

    private async Task<HttpStatusCode> UploadImageAsync(IFormFile file, string path)
    {
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
                return HttpStatusCode.BadRequest;
            }

            //Build path in the web root (better to a specific folder under the web root
            string filePath = $"{this.webHostEnvironment.WebRootPath}{path}{extention}";

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

        return HttpStatusCode.OK;
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



    [HttpPost("updateprofile")]
    public async Task<IActionResult> UpdateProfile([FromBody] TableUserDto userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is null");
        }

        // חיפוש המשתמש לפי Id
        var user = await context.TableUsers.FindAsync(userDto.Id);

        if (user == null)
        {
            return NotFound($"User with ID {userDto.Id} not found");
        }

        // עדכון השדות של המשתמש
        user.UserName = userDto.UserName;
        user.UserLastName = userDto.UserLastName;
        user.UserEmail = userDto.UserEmail;
        user.UserPassword = userDto.UserPassword;
        user.UserPhoneNum = userDto.UserPhoneNum;
        user.CarOwnerId = userDto.CarOwnerId;
       

        try
        {
            // שמירת השינויים למסד הנתונים
            await context.SaveChangesAsync();
            return Ok(new { message = "Profile updated successfully" });
        }
        catch (Exception ex)
        {
            // טיפול בשגיאות
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", error = ex.Message });
        }
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

    private string GetCarImageVirtualPath(string carId)
    {
        string virtualPath = $"/carImages/{carId}";
        string path = $"{this.webHostEnvironment.WebRootPath}\\carImages\\{carId}.png";
        if (System.IO.File.Exists(path))
        {
            virtualPath += ".png";
        }
        else
        {
            path = $"{this.webHostEnvironment.WebRootPath}\\carImages\\{carId}.jpg";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".jpg";
            }
            else
            {
                virtualPath = $"/carImages/default.png";
            }
        }

        return virtualPath;
    }


    [HttpPost("requestCar")]
    public IActionResult RequestCar([FromBody] RequestCarDto requestCarDto)
    {
        try
        {
            // אם ה-DTO הוא null או לא תקין, מחזירים BadRequest
            if (requestCarDto == null)
            {
                return BadRequest("Invalid request data.");
            }

            // יצירת אובייקט RequestCar מתוך ה-DTO
            DriveMeCrazyServer.Models.RequestCar requestCar = requestCarDto.GetModel();

            
            requestCar.StatusId = 2; 
            // שמירת הבקשה בבסיס הנתונים
            context.RequestCars.Add(requestCar);
            context.SaveChanges();

            // יצירת DTO חדש לבקשה שהוזנה
            RequestCarDto createdRequestCarDto = new RequestCarDto(requestCar);

           
            return Ok(createdRequestCarDto);
        }
        catch (Exception ex)
        {
            // אם הייתה שגיאה כלשהי, מחזירים BadRequest עם הודעת השגיאה
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("GetAllCars")]
    public IActionResult GetAllCars()
    {

        try
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model user class from DB with matching email. 
            DriveMeCrazyServer.Models.TableUser? user = context.GetUser(userEmail);
            if (user == null)
            {
                return Unauthorized("User is not logged in");
            }
            List<DriveMeCrazyServer.Models.TableCar> listCars = context.GetAllCar(user.Id);
            List<TableCarDto> output = new List<TableCarDto>();
            foreach (TableCar t in listCars)
            {
                output.Add(new TableCarDto(t));
            }

            return Ok(output);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }




    #region Add Chore
    [HttpPost("AddChore")]
    public IActionResult AddRestaurant([FromBody]ChoreTypeDto choreTypeDto)
    {
        try
        {
            //validate restaurant manager
            ChoresType chore = new ChoresType()
            {
               NameChore=choreTypeDto.NameChore,
               Score=choreTypeDto.Score,
               IdCar=choreTypeDto.IdCar,
            };
            //add restaurant
            context.ChoresTypes.Add(chore);
            context.SaveChanges();
            ChoreTypeDto c =new ChoreTypeDto(chore);    
           
            return Ok(chore);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }

    }
    #endregion





    [HttpGet("GetAllCarRegistred")]
    public IActionResult GetAllRest()
    {
        try
        {
            List<TableCar>? listCar = context.GetAllCars();
            return Ok(listCar);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }





    [HttpGet("GetAllRequestPannding")]
    public IActionResult GetAllRequest()
    {
        try
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model user class from DB with matching email. 
            DriveMeCrazyServer.Models.TableUser? user = context.GetUser(userEmail);
            if (user == null)
            {
                return Unauthorized("User is not logged in");
            }

            int ownerId = user.Id;

            List<RequestCar>? req= context.GetAllRequestStatus2(ownerId);
            List<RequestCarDto> result= new List<RequestCarDto>();
            foreach(RequestCar r in req)
            {
                result.Add(new RequestCarDto(r));
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("ChangeStatusRequestToAprrove")]
    public IActionResult ChangeRestStatusToApprove(RequestCarDto requestDTO)
    {
        try
        {
            //validate its an admin
            string? username = HttpContext.Session.GetString("loggedInUser");
            if (username == null)
                return Unauthorized();
            TableUser? u = context.GetUser(username);
            if (u == null )
                return Unauthorized();

            bool success = context.SetStatus(requestDTO.StatusId, 1);
            if (success)
                return Ok(success);
            else
                return BadRequest("Either requestID not found or DB connection problem!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("ChangeStatusRequestToRegject")]
    public IActionResult ChangeRestStatusToDecline(RequestCarDto requestDTO)
    {
        try
        {
            //validate its an admin
            string? username = HttpContext.Session.GetString("loggedInUser");
            if (username == null)
                return Unauthorized();
            TableUser? u = context.GetUser(username);
            if (u == null)
                return Unauthorized();

            bool success = context.SetStatus(requestDTO.StatusId, 3);
            if (success)
                return Ok(success);
            else
                return BadRequest("Either requestID not found or DB connection problem!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




    [HttpGet("GetAllUserByOwner")]
    public IActionResult GetAllUser()
    {

        try
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model user class from DB with matching email. 
            DriveMeCrazyServer.Models.TableUser? user = context.GetUser(userEmail);
            if (user == null)
            {
                return Unauthorized("User is not logged in");
            }

            List<DriveMeCrazyServer.Models.TableUser> listUsers = context.GetUserByOwner(user.Id);

            List<TableUserDto> result = new List<TableUserDto>();
            foreach (TableUser r in listUsers)
            {
                result.Add(new TableUserDto(r));
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpGet("adddriver")]
    public IActionResult AddDriverCar([FromQuery] string carId, [FromQuery] int userId)
    {
        try
        {
            string? userEmail = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }
            if (carId == null || userId == null)
            {
                return BadRequest("TableCarDto or UserDto is null");
            }

            // יצירת מודל של TableCar מה-DTO
           

            // חיפוש רכב לפי ה-IdCar
         

            // יצירת אובייקט חדש מסוג DriversCar
            var driverCar = new DriversCar
            {
                UserId = userId,
                IdCar =carId,
                Status = 1 // סטטוס ל-1
            };
            TableCar? car = context.GeCarById(carId);
            TableUser? user= context.GetUserById(userId);
            // הוספת קשרים לשורות המתאימות
            driverCar.User = user;
            driverCar.IdCarNavigation = car;

            // הוספת הקשר החדש לטבלת DriversCar
            context.DriversCars.Add(driverCar);

            // שמירה לשינויים במסד הנתונים
            context.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            // במקרה של שגיאה, נשלח הודעת שגיאה עם פרטי השגיאה
            return BadRequest();
        }
    }









}

