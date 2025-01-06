using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

public partial class TableCar
{
    [Key]
    [StringLength(50)]
    public string IdCar { get; set; } = null!;

    public int OwnerId { get; set; }

    [StringLength(50)]
    public string NickName { get; set; } = null!;

    [InverseProperty("IdCarNavigation")]
    public virtual ICollection<ChoresType> ChoresTypes { get; set; } = new List<ChoresType>();

    [InverseProperty("IdCarNavigation")]
    public virtual ICollection<DriversCar> DriversCars { get; set; } = new List<DriversCar>();
}
