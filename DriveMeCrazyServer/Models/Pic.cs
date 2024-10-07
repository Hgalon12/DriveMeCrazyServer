using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

[Table("Pic")]
public partial class Pic
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string NamePic { get; set; } = null!;

    [StringLength(50)]
    public string TextPic { get; set; } = null!;

    [ForeignKey("Id")]
    [InverseProperty("Pic")]
    public virtual PicChore IdNavigation { get; set; } = null!;
}
