namespace DriveMeCrazyServer.DTO
{
    public class TableCarDto
    {
        public int IdCar { get; set; }
        public int TypeId { get; set; }
        public int NumOfPlaces { get; set; }
        public int OwnerId { get; set; }
        public string NickName { get; set; } = null;
        public int NumOfCars { get; set; }
        public TableCarDto(Models.TableCar modelCar ) 
        {
            this.IdCar=modelCar.IdCar;
            this.TypeId=modelCar.TypeId;
            this.NumOfPlaces = modelCar.NumOfPlaces;
            this.OwnerId=modelCar.OwnerId;
            this.NickName=modelCar.NickName;
            this.NumOfCars = modelCar.NumOfCars;
        }
        public Models.TableCar GetModel()
        {
            Models.TableCar car = new Models.TableCar();
            car.IdCar = this.IdCar;
            car.TypeId = this.TypeId;
            car.NumOfPlaces=this.NumOfPlaces;
            car.OwnerId=this.OwnerId;
            car.NickName=this.NickName;
            car.NumOfCars=this.NumOfCars;
            return car;
        }
            
            
            
            
            
            }
}
