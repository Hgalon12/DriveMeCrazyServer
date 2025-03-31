Use master
Go

-- Create a login for the admin user
CREATE LOGIN [CarsAdminLogin] WITH PASSWORD = 'Hg2501';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [CarsAdminLogin];
Go

CREATE DATABASE DriveMeCrazyDB
GO

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