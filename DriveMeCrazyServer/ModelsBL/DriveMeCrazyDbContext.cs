using System;
using System.Collections.Generic;
using Azure.Core;
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
    public TableUser? GetUserById(int id)
    {
        return this.TableUsers.Where(u => u.Id == id)
                            .FirstOrDefault();
    }
    public TableCar? GeCarById(string id)
    {
        return this.TableCars.Where(c => c.IdCar==id).FirstOrDefault();
    }
    
    public List<TableUser> GetUserByOwner(int id)
    {
        return this.TableUsers.Where(u => u.CarOwnerId== id )
                            .ToList();
    }
    public List<TableCar> GetAllCar(int id)
    {
        return this.TableCars.Where(c => c.DriversCars.Where(d => d.UserId == id && d.Status == 1).Any()).ToList();
    }
    public List<TableCar>? GetAllCars()
    {
        return this.TableCars.ToList();
    }
    public List<RequestCar>? GetAllRequest()
    {
        return this.RequestCars.ToList();
    }
    public List<RequestCar>? GetAllRequestStatus2(int ownerId)
    {
        List<RequestCar> list =  this.RequestCars.Include(r => r.DriversCar).ThenInclude(d => d.User)
            .Where(r => r.StatusId == 2)
            .ToList();

        List<TableCar> cars = TableCars.Where(t => t.OwnerId == ownerId).ToList();

        List<RequestCar> output = new List<RequestCar>();
        foreach (RequestCar r in list)
        {
            if (cars.Exists(c => c.IdCar == r.IdCar))
            {
                output.Add(r);
            }
        }
        return output;
    }
    public RequestCar? GetRequestByStatus( int requestId)
    {
        return this.RequestCars
             .FirstOrDefault(r => r.RequestId == requestId && r.StatusId == 2);

    }
    public bool SetStatus(int requestId, int statusId)
    {
        try
        {
            RequestCar? r = this.RequestCars.Where(r => r.RequestId == requestId).FirstOrDefault();
            if (r != null)
            {
                r.StatusId = statusId;
                this.Update(r);
                this.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            return false;
        }

    }
  
}

