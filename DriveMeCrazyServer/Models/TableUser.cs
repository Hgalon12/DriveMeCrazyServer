using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Index("UserEmail", Name = "UQ__TableUse__08638DF8A13CAD08", IsUnique = true)]
public partial class TableUser
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string CarId { get; set; } = null!;

    [StringLength(50)]
    public string UserLastName { get; set; } = null!;

    [StringLength(50)]
    public string UserEmail { get; set; } = null!;

    [StringLength(50)]
    public string UserPassword { get; set; } = null!;

    [StringLength(50)]
    public string UserPhoneNum { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<DriversCar> DriversCars { get; set; } = new List<DriversCar>();

    [InverseProperty("Owner")]
    public virtual ICollection<TableCar> TableCars { get; set; } = new List<TableCar>();
}
