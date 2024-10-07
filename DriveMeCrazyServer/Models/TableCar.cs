using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

public partial class TableCar
{
    public int NumOfCars { get; set; }

    [Key]
    public int IdCar { get; set; }

    [Column("TypeID")]
    public int TypeId { get; set; }

    public int NumOfPlaces { get; set; }

    public int OwnerId { get; set; }

    [StringLength(50)]
    public string NickName { get; set; } = null!;

    [InverseProperty("IdCarNavigation")]
    public virtual ICollection<ChoresType> ChoresTypes { get; set; } = new List<ChoresType>();

    [InverseProperty("IdCarNavigation")]
    public virtual ICollection<DriversCar> DriversCars { get; set; } = new List<DriversCar>();

    [ForeignKey("OwnerId")]
    [InverseProperty("TableCars")]
    public virtual TableUser Owner { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("TableCars")]
    public virtual CarType Type { get; set; } = null!;
}
