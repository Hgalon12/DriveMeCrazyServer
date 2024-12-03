using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Table("StatusCar")]
public partial class StatusCar
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string DescriptionCar { get; set; } = null!;

    [InverseProperty("StatusNavigation")]
    public virtual ICollection<DriversCar> DriversCars { get; set; } = new List<DriversCar>();

    [InverseProperty("Status")]
    public virtual ICollection<RequestCar> RequestCars { get; set; } = new List<RequestCar>();
}
