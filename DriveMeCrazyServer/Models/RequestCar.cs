using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Table("RequestCar")]
public partial class RequestCar
{
    public int UserId { get; set; }

    [StringLength(50)]
    public string IdCar { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? WhenIneedthecar { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UntilWhenIneedthecar { get; set; }

    [StringLength(50)]
    public string Reason { get; set; } = null!;

    [Key]
    public int RequestId { get; set; }

    public int StatusId { get; set; }

    [ForeignKey("UserId, IdCar")]
    [InverseProperty("RequestCars")]
    public virtual DriversCar DriversCar { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("RequestCars")]
    public virtual StatusCar Status { get; set; } = null!;
}
