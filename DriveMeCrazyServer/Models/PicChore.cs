using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

public partial class PicChore
{
    [Key]
    public int PicId { get; set; }

    public int AssignmentId { get; set; }

    [ForeignKey("AssignmentId")]
    [InverseProperty("PicChores")]
    public virtual Report Assignment { get; set; } = null!;

    [InverseProperty("IdNavigation")]
    public virtual Pic? Pic { get; set; }
}
