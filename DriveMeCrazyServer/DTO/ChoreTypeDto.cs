using Microsoft.Identity.Client;

namespace DriveMeCrazyServer.DTO
{
    public class ChoreTypeDto
    {
        public int ChoreId { get; set; }

        public string NameChore { get; set; } = null;

        public int Score { get; set; }

        public string IdCar { get; set; } = null!;
        public ChoreTypeDto(Models.ChoresType modelChore)
        {
            this.ChoreId = modelChore.ChoreId;
            this.NameChore = modelChore.NameChore;
            this.Score = modelChore.Score;
            this.IdCar = modelChore.IdCar;
        }
        public Models.ChoresType GetModel()
        {
            Models.ChoresType c = new Models.ChoresType();
            c.ChoreId=this.ChoreId;
            c.IdCar=this.IdCar;
            c.NameChore=this.NameChore;
            c.Score=this.Score;
            return c;
        }

    }
}
