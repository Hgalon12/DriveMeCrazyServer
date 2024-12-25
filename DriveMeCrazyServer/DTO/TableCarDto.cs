namespace DriveMeCrazyServer.DTO
{
    public class TableCarDto
    {
         public string IdCar { get; set; } = null!;
        public int TypeId { get; set; }
        public int NumOfPlaces { get; set; }
        public int OwnerId { get; set; }
        public string NickName { get; set; } = null!;

        public string CarImagePath { get; set; } = "";
        
        public TableCarDto() { }
        public TableCarDto(Models.TableCar modelCar ) 
        {
            this.IdCar=modelCar.IdCar;
            this.OwnerId=modelCar.OwnerId;
            this.NickName=modelCar.NickName;
        }
        public Models.TableCar GetModel()
        {
            Models.TableCar car = new Models.TableCar();
            car.IdCar = this.IdCar;
            car.OwnerId=this.OwnerId;
            car.NickName=this.NickName;
            return car;
        }
            
            
            
            
            
            }
}
