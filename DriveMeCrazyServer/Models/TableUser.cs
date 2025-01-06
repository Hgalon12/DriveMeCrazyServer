using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Index("UserEmail", Name = "UQ__TableUse__08638DF810FC2291", IsUnique = true)]
public partial class TableUser
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    public int? CarOwnerId { get; set; }

    [StringLength(50)]
    public string UserLastName { get; set; } = null!;

    [StringLength(50)]
    public string UserEmail { get; set; } = null!;

    [StringLength(50)]
    public string UserPassword { get; set; } = null!;

    [StringLength(50)]
    public string UserPhoneNum { get; set; } = null!;

    [ForeignKey("CarOwnerId")]
    [InverseProperty("InverseCarOwner")]
    public virtual TableUser? CarOwner { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<DriversCar> DriversCars { get; set; } = new List<DriversCar>();

    [InverseProperty("CarOwner")]
    public virtual ICollection<TableUser> InverseCarOwner { get; set; } = new List<TableUser>();
}
