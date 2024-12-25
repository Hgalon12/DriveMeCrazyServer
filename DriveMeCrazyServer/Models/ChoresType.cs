using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Table("ChoresType")]
public partial class ChoresType
{
    [StringLength(50)]
    public string NameChore { get; set; } = null!;

    public int Score { get; set; }

    [Key]
    public int ChoreId { get; set; }

    [StringLength(50)]
    public string IdCar { get; set; } = null!;

    [ForeignKey("IdCar")]
    [InverseProperty("ChoresTypes")]
    public virtual TableCar IdCarNavigation { get; set; } = null!;

    [InverseProperty("Chore")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
