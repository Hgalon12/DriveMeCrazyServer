﻿use master
Go

IF EXISTS (SELECT * FROM sys.databases WHERE name = N'DriveMeCrazyDB')

BEGIN 

DROP DATABASE DriveMeCrazyDB;

END

Go

Create Database DriveMeCrazyDB

Go
Use DriveMeCrazyDB

Go

CREATE TABLE TableUsers 
(
    Id INT PRIMARY KEY IDENTITY,
    UserName NVARCHAR(50) NOT NULL,
    CarId NVARCHAR(50) NOT NULL,
    UserLastName NVARCHAR(50) NOT NULL,
    UserEmail NVARCHAR(50) UNIQUE NOT NULL,
    UserPassword NVARCHAR(50) NOT NULL,
    InsurantNum NVARCHAR(50) NOT NULL,
    UserPhoneNum NVARCHAR(50) NOT NULL
);

CREATE TABLE StatusCar
(
    Id INT PRIMARY KEY IDENTITY, 
    DescriptionCar NVARCHAR(50) NOT NULL
);

CREATE TABLE CarType
(
    IdCarType INT NOT NULL PRIMARY KEY, 
    TypeName NVARCHAR(50) NOT NULL
);

CREATE TABLE TableCars
(
    NumOfCars INT NOT NULL,
    IdCar INT NOT NULL PRIMARY KEY,
    TypeID INT NOT NULL,
    NumOfPlaces INT NOT NULL,
    OwnerId INT NOT NULL,  -- Ensure this is INT to match TableUsers.Id
    NickName NVARCHAR(50) NOT NULL,
    FOREIGN KEY (TypeID) REFERENCES CarType(IdCarType),
    FOREIGN KEY (OwnerId) REFERENCES TableUsers(Id)  -- Ensure consistency
);

CREATE TABLE DriversCar
(
    UserId INT NOT NULL,
    IdCar INT NOT NULL,
    PRIMARY KEY (UserId, IdCar),
    FOREIGN KEY (UserId) REFERENCES TableUsers(Id),
    FOREIGN KEY (IdCar) REFERENCES TableCars(IdCar)
);

CREATE TABLE ChoresType
(
    NameChore NVARCHAR(50) NOT NULL,
    Score INT NOT NULL,
    ChoreId INT PRIMARY KEY IDENTITY,
    IdCar INT NOT NULL,
    FOREIGN KEY (IdCar) REFERENCES TableCars(IdCar)
);

CREATE TABLE Report
(
    Score INT NOT NULL,
    AssignmentId INT PRIMARY KEY IDENTITY,
    ChoreId INT NOT NULL,
    UserId INT NOT NULL,
    IdCar INT NOT NULL,
    ReportDate DATE,
    FOREIGN KEY (UserId, IdCar) REFERENCES DriversCar(UserId, IdCar),  -- Reference composite key
    FOREIGN KEY (ChoreId) REFERENCES ChoresType(ChoreId)
);


CREATE TABLE PicChores
(
    PicId INT PRIMARY KEY IDENTITY, 
    AssignmentId INT NOT NULL,
    FOREIGN KEY (AssignmentId) REFERENCES Report(AssignmentId)
);

CREATE TABLE Pic
(
    Id INT NOT NULL,
    NamePic NVARCHAR(50) NOT NULL,
    TextPic NVARCHAR(50) NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (Id) REFERENCES PicChores(PicId)
);

CREATE TABLE RequestCar
(
    UserId INT NOT NULL,
    IdCar INT NOT NULL,
    WhenIneedthecar NVARCHAR(50) NOT NULL,
    Reason NVARCHAR(50) NOT NULL,
    RequestId INT PRIMARY KEY IDENTITY, 
    StatusId INT NOT NULL,
    FOREIGN KEY (UserId, IdCar) REFERENCES DriversCar(UserId, IdCar),  -- Reference composite key
    FOREIGN KEY (StatusId) REFERENCES StatusCar(Id)
);

CREATE LOGIN [CarsAdminLogin] WITH PASSWORD = 'Hg2501';
Go

CREATE USER [CarsAdminUser] FOR LOGIN
[CarsAdminLogin];

Go
ALTER ROLE db_owner ADD MEMBER [CarsAdminUser];

Go


SELECT * FROM TableUsers

--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog= DriveMeCrazyDB;User ID=CarsAdminLogin;Password=Hg2501;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context DriveMeCrazyDbContext -DataAnnotations –force