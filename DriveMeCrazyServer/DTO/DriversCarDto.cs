namespace DriveMeCrazyServer.DTO
{
    public class DriversCarDto
    {
        
            public int UserId { get; set; }

            public string IdCar { get; set; } = null!;
        public int Status { get; set; }
        public DriversCarDto(Models.DriversCar modelDriver)
        {
            this.UserId = modelDriver.UserId;
            this.IdCar = modelDriver.IdCar;


        }
         public Models.DriversCar GetModel()
        {
            Models. DriversCar drivers = new Models.DriversCar();
            drivers.UserId = this.UserId;
            drivers.IdCar = this.IdCar;
            return drivers;
        }
     }

    }

