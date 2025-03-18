namespace DriveMeCrazyServer.DTO
{
    public class TableCarDto
    {
         public string IdCar { get; set; } = null!;
     
       
        public int OwnerId { get; set; }
        public string NickName { get; set; } = null!;

        public string CarImagePath { get; set; } = "";
        
        public TableCarDto() { }
        public TableCarDto(Models.TableCar modelCar, string rootPath = "" ) 
        {
            this.IdCar=modelCar.IdCar;
            this.OwnerId=modelCar.OwnerId;
            this.NickName=modelCar.NickName;
            this.CarImagePath = GetCarImageVirtualPath(this.IdCar, rootPath);
        }
        public Models.TableCar GetModel()
        {
            Models.TableCar car = new Models.TableCar();
            car.IdCar = this.IdCar;
            car.OwnerId=this.OwnerId;
            car.NickName=this.NickName;
            return car;
        }



        private string GetCarImageVirtualPath(string carId, string rootPath = "")
        {
            string virtualPath = $"/carImages/{carId}";
            string path = $"{rootPath}\\carImages\\{carId}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                path = $"{rootPath}\\carImages\\{carId}.jpg";
                if (System.IO.File.Exists(path))
                {
                    virtualPath += ".jpg";
                }
                else
                {
                    virtualPath = $"/carImages/car.png";
                }
            }

            return virtualPath;
        }

    }
}
