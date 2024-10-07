using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Table("CarType")]
public partial class CarType
{
    [Key]
    public int IdCarType { get; set; }

    [StringLength(50)]
    public string TypeName { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<TableCar> TableCars { get; set; } = new List<TableCar>();
}
