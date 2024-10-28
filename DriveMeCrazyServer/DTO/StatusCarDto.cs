namespace DriveMeCrazyServer.DTO
{
    public class StatusCarDto
    { 
        public int Id { get; set; }
        public string DescriptionCar { get; set; } = null;
       
        public StatusCarDto(Models.StatusCar statusCar)
        {
            this.Id = statusCar.Id;
            this.DescriptionCar = statusCar.DescriptionCar;

        }
       public Models.StatusCar StatusCar()
        {
            Models.StatusCar statusCar = new Models.StatusCar();
            statusCar.Id = this.Id;
            statusCar.DescriptionCar=this.DescriptionCar;
            return statusCar;
        }
    }
    
}
