namespace DriveMeCrazyServer.DTO
{
    public class CarTypeDto
    {

            public int IdCarType { get; set; }

            public string TypeName { get; set; } = null;
        public CarTypeDto(Models.CarType modelCarType)
        {
            this.IdCarType=modelCarType.IdCarType;
            this.TypeName = modelCarType.TypeName;
        }
        public Models.CarType GetModel()
        {
            Models.CarType c=new Models.CarType();  
            c.IdCarType = this.IdCarType;
            c.TypeName = this.TypeName;
            return c;
        }
    

    }
}
