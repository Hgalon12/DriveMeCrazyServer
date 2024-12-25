using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Table("Report")]
public partial class Report
{
    public int Score { get; set; }

    [Key]
    public int AssignmentId { get; set; }

    public int ChoreId { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    public string IdCar { get; set; } = null!;

    public DateOnly? ReportDate { get; set; }

    [ForeignKey("ChoreId")]
    [InverseProperty("Reports")]
    public virtual ChoresType Chore { get; set; } = null!;

    [ForeignKey("UserId, IdCar")]
    [InverseProperty("Reports")]
    public virtual DriversCar DriversCar { get; set; } = null!;

    [InverseProperty("Assignment")]
    public virtual ICollection<PicChore> PicChores { get; set; } = new List<PicChore>();
}
