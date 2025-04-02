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
DELETE FROM TableCars
WHERE (IdCar = '123432' AND OwnerId = 3 AND NickName = 'eeee') OR
      (IdCar = '2222' AND OwnerId = 1 AND NickName = 'car') OR
      (IdCar = '222244' AND OwnerId = 3 AND NickName = 'dsddd') OR
      
      (IdCar = '32327' AND OwnerId = 3 AND NickName = 'ff') OR
      (IdCar = '3333' AND OwnerId = 1 AND NickName = 'car1') OR
      (IdCar = '4343w' AND OwnerId = 3 AND NickName = 'amit') OR
      (IdCar = '474747' AND OwnerId = 3 AND NickName = 'hadasgalon') OR
      (IdCar = '544' AND OwnerId = 3 AND NickName = 'jh') OR
      (IdCar = '6666' AND OwnerId = 3 AND NickName = 'fdf') OR
      (IdCar = '7653234' AND OwnerId = 3 AND NickName = 'hghg') OR
      
      (IdCar = '8777' AND OwnerId = 3 AND NickName = 'rotem') OR
      (IdCar = '99887' AND OwnerId = 3 AND NickName = 'uuuyy') OR
      (IdCar = 'ttt' AND OwnerId = 3 AND NickName = 'ooo');

