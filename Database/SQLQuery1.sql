﻿create database BTLWeb
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
CREATE PROCEDURE searchMotel(
    @Address_search VARCHAR(255),
    @min_price_search INT,
    @max_price_search INT
)
AS
BEGIN
    IF @Address_search IS NOT NULL AND @min_price_search = 0 AND @max_price_search = 0
    BEGIN
        SELECT * FROM tblMotel WHERE Address LIKE CONCAT('%', @Address_search, '%');
    END
    ELSE IF @Address_search IS NOT NULL AND @min_price_search IS NOT NULL AND @max_price_search IS NOT NULL
    BEGIN
        SELECT * FROM tblMotel WHERE Address LIKE CONCAT('%', @Address_search, '%') AND Price BETWEEN @min_price_search AND @max_price_search;
    END
    ELSE IF @Address_search IS NULL AND @min_price_search IS NOT NULL AND @max_price_search IS NOT NULL
    BEGIN
        SELECT * FROM tblMotel WHERE Price BETWEEN @min_price_search AND @max_price_search;
    END
END;
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
delete from tblFavourite
where Id=4;