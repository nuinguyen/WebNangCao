create database BTLWeb
go 
use BTLWeb
go

CREATE TABLE tblStaff (
    Id int PRIMARY KEY IDENTITY,
    Name nvarchar(50) NOT NULL,
    Email nvarchar(100) NOT NULL,
    Birthday date ,
    Gender varchar(10),
    Phone varchar(20),
    Address nvarchar(200),
    Password varchar(200),
);
go
INSERT INTO tblStaff (Name, Email, Birthday, Gender, Phone, Address, Password)
VALUES ('Nui', 'nui@gmail.com', '2001-09-09', 'male', '09382626622', N'Thái Bình', '12345'),
       ('Manh', 'manh@gmail.com', '2000-08-08', 'female', '0109393913', N'Hà Nội', '12345'),
       ('Quyen', 'quyen@gmail.com', '1999-07-07', 'male', '0393713131', N'Phú Thọ', '12345');

go
select * from tblStaff
go
go
CREATE TABLE tblMotel (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    User_id INT,
    Title nvarchar(200),
	Description nvarchar(1000),
    Acreage FLOAT,
    Price FLOAT,
    Image varchar(200),
    Address_post nvarchar(1000),
    Name nvarchar(1000),
    Phone varchar(200),
    Address nvarchar(200),
    Email nvarchar(200),
    Date_created Datetime,
    Status int,
);
go
INSERT INTO tblMotel (User_id,Title, Description, Acreage, Price, Image, Address_post, Name,Phone,Address,Email,Date_created,status )
VALUES (1,N'Phòng mới xây đẹp', N'Đẹp', 20, 1500000, 'image', N'69 ĐỊnh Công Hà Nội',N'Mạnh','098899876','Nam ĐỊnh','manh@gmail.com',GETDATE(),1),
       (2,N'Phòng Chính chủ', N'Đẹp', 35, 1400000, 'image', N'Hoàng Mai Hà Nội',N'QUyền','012345678','Thái Bình','quyen@gmail.com',GETDATE(),1);

go
go
select * from tblMotel
go

CREATE TABLE tblUser (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name  nvarchar(200),
    Email nvarchar(200),
	Phone varchar(200),
    Password varchar(200),
);
go
INSERT INTO tblUser (Name, Email, Phone, Password)
VALUES (N'Núi','nui@gmail.com', '0192828282', '54321'),
        (N'Quyền','quyen@gmail.com', '0322212212', '54321'),
       (N'Mạnh','manh@gmail.com', '0201011121', '54321');
go
select * from tblUser

go


CREATE TABLE tblFavourite (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    User_id  INT,
);
go
select * from tblFavourite
go


CREATE TABLE tblFavourite_detail (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Favourite_id  INT,
    Motel_id  INT,
);
go
select * from tblFavourite_detail
go

CREATE TABLE tblCity(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name  nvarchar(200),
);
go
CREATE TABLE tblDistrict(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name  nvarchar(200),
    City_id int,
);
go
INSERT INTO tblDistrict (Name, City_id)
VALUES (N'Hai Bà Trưng',1),
        (N'Cầu Giấy',1),
        (N'Hoàng Mai',1),
        (N'Thanh Xuân',1),
        (N'Quận 1',2),
        (N'Quận 2',2),
        (N'Quận 3',2),
        (N'Hội An',3),
       (N'Xuân Thủy',3);
go
select * from tblDistrict
go
CREATE TABLE tblVillage(
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name  nvarchar(200),
	District_id int,
);
go
INSERT INTO tblVillage (Name, District_id)
VALUES (N'Định Công',1),
        (N'Giải Phóng',1),
        (N'HOàng Diệu',1),
        (N'Thanh Xuân',1),
        (N'Quang Trung',5),
        (N'Phú Trọng',5),
        (N'Ngô Quyền',6),
       (N'La Thăng',9);
go
