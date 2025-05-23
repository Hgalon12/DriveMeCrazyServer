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
    CarOwnerId int NULL,
    UserLastName NVARCHAR(50) NOT NULL,
    UserEmail NVARCHAR(50) UNIQUE NOT NULL,
    UserPassword NVARCHAR(50) NOT NULL,
    UserPhoneNum NVARCHAR(50) NOT NULL,
    FOREIGN KEY (CarOwnerId) REFERENCES TableUsers(Id) 
);

CREATE TABLE StatusCar
(
    Id INT PRIMARY KEY IDENTITY, 
    DescriptionCar NVARCHAR(50) NOT NULL
);


CREATE TABLE TableCars
(
    IdCar NVARCHAR(50) NOT NULL PRIMARY KEY,
    OwnerId INT NOT NULL,  -- Ensure this is INT to match TableUsers.Id
    NickName NVARCHAR(50) NOT NULL,
  
  -- Ensure consistency
);

CREATE TABLE DriversCar
(
    UserId INT NOT NULL,
    IdCar NVARCHAR(50)  NOT NULL,
    [Status] int not null FOREIGN KEY REFERENCES StatusCar(Id),
    PRIMARY KEY (UserId, IdCar),
    FOREIGN KEY (UserId) REFERENCES TableUsers(Id),
    FOREIGN KEY (IdCar) REFERENCES TableCars(IdCar)
);

CREATE TABLE ChoresType
(
    NameChore NVARCHAR(50) NOT NULL,
    Score INT NOT NULL,
    ChoreId INT PRIMARY KEY IDENTITY,
    IdCar NVARCHAR(50)  NOT NULL,
    FOREIGN KEY (IdCar) REFERENCES TableCars(IdCar)
);

CREATE TABLE Report
(
    Score INT NOT NULL,
    AssignmentId INT PRIMARY KEY IDENTITY,
    ChoreId INT NOT NULL,
    UserId INT NOT NULL,
    IdCar NVARCHAR(50) NOT NULL,
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
    IdCar NVARCHAR(50) NOT NULL,
    WhenIneedthecar DateTime,
    UntilWhenIneedthecar DateTime,
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
insert into TableUsers values('Hadas',1,'Galon','Hadas@gmail.com','1111','0546287507')
insert into TableCars values('1234',1,'suzi')
insert into TableCars values('1111',1,'yondi')
insert into TableCars values('2222',1,'car')
insert into TableCars values('3333',1,'car1')
insert into TableUsers values('כככ',1,'Galon','עע@gmail.com','3333','0546287507')


insert into StatusCar values('approve')
insert into StatusCar values('pennding')
insert into StatusCar values('rejected') 
insert into ChoresType values ('Car wash','100','1234')
insert into DriversCar values(1,'1234',1)
insert into DriversCar values(1,'1111',1)
insert into DriversCar values(1,'2222',1)
insert into DriversCar values(4,'4444',1)
select * from TableCars
select * from ChoresType
select *from StatusCar
select* from RequestCar
select * from DriversCar
select* from TableCars

delete DriversCar where UserId=1 and IdCar =('2222')


DELETE FROM TableCars
WHERE 
     

      
      (IdCar = 'ttt' AND OwnerId = 3 AND NickName = 'ooo');


      select * from tableCars
      select * from DriversCar


--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog= DriveMeCrazyDB;User ID=CarsAdminLogin;Password=Hg2501;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context DriveMeCrazyDbContext -DataAnnotations –force