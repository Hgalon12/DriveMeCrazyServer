using System;
using System.Collections.Generic;
using DriveMeCrazyServer.DTO;
using Microsoft.EntityFrameworkCore;

namespace DriveMeCrazyServer.Models;

public partial class DriveMeCrazyDbContext : DbContext
{
    public TableUser? GetUser(string email)
    {
        return this.TableUsers.Where(u => u.UserEmail == email)
                            .FirstOrDefault();
    }
    public List<TableCar> GetAllCar(int id)
    {
        return this.TableCars.Where(c => c.DriversCars.Where(d => d.UserId == id && d.Status == 1).Any()).ToList();
    }
    public List<TableCar>? GetAllCars()
    {
        return this.TableCars.ToList();
    }
}

