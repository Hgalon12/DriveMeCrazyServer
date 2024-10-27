namespace DriveMeCrazyServer.DTO
{
    public class TableUserDto
    {
        
            public int Id { get; set; }

            public string UserName { get; set; } = null;

        public string CarId { get; set; } = null;

        public string UserLastName { get; set; } = null;

        public string UserEmail { get; set; } = null;

        public string UserPassword { get; set; } = null;

        public string InsurantNum { get; set; } = null;

        public string UserPhoneNum { get; set; } = null;
        public TableUserDto(Models.TableUser modleUser)
        {
            this.Id = modleUser.Id;
            this.UserName = modleUser.UserName;
            this.CarId = modleUser.CarId;
            this.UserLastName = modleUser.UserLastName;
            this.UserEmail = modleUser.UserEmail;
            this.UserPassword = modleUser.UserPassword;
            this.InsurantNum = modleUser.InsurantNum;
            this.UserPhoneNum = modleUser.UserPhoneNum;

        }
        public Models.TableUser GetModel()
        {
            Models.TableUser u= new Models.TableUser(); 
            u.Id = this.Id;
            u.UserName = this.UserName;
            u.CarId = this.CarId;
            u.UserLastName = this.UserLastName;
            u.UserEmail = this.UserEmail;
            u.UserPassword = this.UserPassword;
            u.InsurantNum= this.InsurantNum;
            u.UserPhoneNum= this.UserPhoneNum;
            return u;
        }

     }
     
    }

