using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[PrimaryKey("UserId", "IdCar")]
[Table("DriversCar")]
public partial class DriversCar
{
    [Key]
    public int UserId { get; set; }

    [Key]
    public int IdCar { get; set; }

    [ForeignKey("IdCar")]
    [InverseProperty("DriversCars")]
    public virtual TableCar IdCarNavigation { get; set; } = null!;

    [InverseProperty("DriversCar")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [InverseProperty("DriversCar")]
    public virtual ICollection<RequestCar> RequestCars { get; set; } = new List<RequestCar>();

    [ForeignKey("UserId")]
    [InverseProperty("DriversCars")]
    public virtual TableUser User { get; set; } = null!;
}
