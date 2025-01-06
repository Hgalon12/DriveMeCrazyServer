namespace DriveMeCrazyServer.DTO
{
    public class RequestCarDto
    {
        
            public int RequestId { get; set; }

            public int UserId { get; set; }

             public string IdCar { get; set; } = null!;

        public DateTime? WhenIneedthecar { get; set; }

        public string Reason { get; set; } = null;

            public int StatusId { get; set; }
        public RequestCarDto() { }
        public RequestCarDto(Models.RequestCar requestCar)
        {
            this.RequestId = requestCar.RequestId;
            this.UserId = requestCar.UserId;
             this.IdCar = requestCar.IdCar;
            this.WhenIneedthecar=requestCar.WhenIneedthecar;
            this.Reason = requestCar.Reason;
            this.StatusId = requestCar.StatusId;

        }
        public  Models.RequestCar GetModel()
        {

          Models.RequestCar request = new Models.RequestCar();
          request.RequestId = this.RequestId;
          request.UserId = this.UserId;
          request.IdCar = this.IdCar;
          request.WhenIneedthecar = this.WhenIneedthecar;
          request.Reason = this.Reason;
          request.StatusId = this.StatusId;

            return request;
        }

    }
}
