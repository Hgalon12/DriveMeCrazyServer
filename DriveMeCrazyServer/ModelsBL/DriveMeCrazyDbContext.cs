using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

public partial class DriveMeCrazyDbContext : DbContext
{
    public TableUser? GetUser(string email)
    {
        return this.TableUsers.Where(u => u.UserEmail == email)
                            .FirstOrDefault();
    }
}

