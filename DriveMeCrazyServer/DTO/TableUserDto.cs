namespace DriveMeCrazyServer.DTO
{
    public class TableUserDto
    {
        public TableUserDto() { }
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public int? CarOwnerId { get; set; }
        public string? CarOwnerEmail { get; set; }


        public string UserLastName { get; set; } = null!;

        public string UserEmail { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

        public string UserPhoneNum { get; set; } = null!;
        public string ProfileImagePath { get; set; } = "";
        public bool IsManager { get; set; }
        public TableUserDto(Models.TableUser modleUser)
        {
            this.Id = modleUser.Id;
            this.UserName = modleUser.UserName;
            this.CarOwnerId = modleUser.CarOwnerId;
            this.UserLastName = modleUser.UserLastName;
            this.UserEmail = modleUser.UserEmail;
            this.UserPassword = modleUser.UserPassword;
            this.UserPhoneNum = modleUser.UserPhoneNum;

        }
        public Models.TableUser GetModel()
        {
            Models.TableUser u= new Models.TableUser(); 
            u.Id = this.Id;
            u.UserName = this.UserName;
            u.CarOwnerId = this.CarOwnerId;
            u.UserLastName = this.UserLastName;
            u.UserEmail = this.UserEmail;
            u.UserPassword = this.UserPassword;
            u.UserPhoneNum= this.UserPhoneNum;
            return u;
        }

     }
     
    }

