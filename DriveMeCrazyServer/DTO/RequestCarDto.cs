using DriveMeCrazyServer.Models;

namespace DriveMeCrazyServer.DTO
{
    public class RequestCarDto
    {
        
            public int RequestId { get; set; }

            public int UserId { get; set; }

        public TableUserDto? Requester { get; set; }

        public string IdCar { get; set; } = null!;

        public DateTime? WhenIneedthecar { get; set; }
        public DateTime? UntilWhenIneedthecar { get; set; }
        public string Reason { get; set; } = null;
        public TableCarDto Car { get; set; } = null;

            public int StatusId { get; set; }
        public RequestCarDto() { }
        public RequestCarDto(Models.RequestCar requestCar)
        {
            this.RequestId = requestCar.RequestId;
            this.UserId = requestCar.UserId;
             this.IdCar = requestCar.IdCar;
            this.WhenIneedthecar=requestCar.WhenIneedthecar;
            this.UntilWhenIneedthecar = requestCar.UntilWhenIneedthecar;
            this.Reason = requestCar.Reason;
            this.StatusId = requestCar.StatusId;
            if (requestCar.DriversCar != null && requestCar.DriversCar.User != null)
                this.Requester = new TableUserDto(requestCar.DriversCar.User);
            else
                this.Requester = null;
            if (requestCar.DriversCar != null && requestCar.DriversCar.IdCarNavigation != null)
                this.Car = new TableCarDto(requestCar.DriversCar.IdCarNavigation);
            else
                this.Car = null;

        }
        public  Models.RequestCar GetModel()
        {

          Models.RequestCar request = new Models.RequestCar();
          request.RequestId = this.RequestId;
          request.UserId = this.UserId;
          request.IdCar = this.IdCar;
          request.WhenIneedthecar = this.WhenIneedthecar;
           request.UntilWhenIneedthecar=this.UntilWhenIneedthecar;
          request.Reason = this.Reason;
          request.StatusId = this.StatusId;

            return request;
        }

    }
}
