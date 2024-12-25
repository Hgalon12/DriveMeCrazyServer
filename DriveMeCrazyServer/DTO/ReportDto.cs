namespace DriveMeCrazyServer.DTO
{
    public class ReportDto
    {
      
            public int AssignmentId { get; set; }

            public int Score { get; set; }

            public int ChoreId { get; set; }

            public int UserId { get; set; }

            public string IdCar { get; set; } = null!;

          public DateOnly? ReportDate { get; set; }
         public ReportDto(Models.Report modelReport)
        {
            this.AssignmentId = modelReport.AssignmentId;
            this.Score = modelReport.Score;
            this.ChoreId = modelReport.ChoreId;
            this.UserId = modelReport.UserId;
            this.IdCar = modelReport.IdCar;
            this.ReportDate = modelReport.ReportDate;

        }

        public Models.Report GetModel()
        {
            Models.Report report = new Models.Report();
            report.AssignmentId = this.AssignmentId;    
            report.Score = this.Score;
            report.ChoreId= this.ChoreId;
            report.UserId = this.UserId;
            report.IdCar = this.IdCar;
            report.ReportDate = this.ReportDate; 
            return report;

        }
    }
}
