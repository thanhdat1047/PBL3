CREATE DATABASE QuanLyThuVienSach
GO

USE QuanLyThuVienSach
GO

create table Position(
ID_Position int primary key,
Name_Position varchar(20)
)
GO 

create table Account(
ID_Account int identity(1, 1) primary key,
UserName varchar(30) unique,
Password varchar(50),
ID_Position int references dbo.Position(ID_Position),
state bit,
)
go

create table Person(
ID_Person int identity(1, 1) primary key,
Name_Person varchar(30),
Gender varchar(30),
DateOfBirth DATE,
Address varchar(40),
Email varchar(50),
PhoneNumber varchar(50),
ID_Account int references Account(ID_Account) UNIQUE
)
GO

create table Sach(
MaSach int identity(1, 1) primary key,
TenSach varchar(100),
Theloai varchar(20),
TenTacGia varchar(40),
SolanTaiBan char(11),
NamXuatBan char(4),
GiaNhap int,
GiaBan int,
state bit,
)
go

create table Kho(
MaSach int primary key foreign key (MaSach) references Sach,
TongSoLuong int
)
go

create table LichSuNhapSach(
ID_LichSuNhapSach int identity(1, 1) primary key,
MaSach int foreign key (MaSach) references Kho,
SoLuong int,
NgayNhap datetime,
ID_Person INT,
FOREIGN KEY (ID_Person) REFERENCES dbo.Person(ID_Person),
)
go

create table SachKhuyenMai(
ID_SachKhuyenMai INT IDENTITY(1, 1) PRIMARY KEY,
MaSach int foreign key (MaSach) references Sach,
MucGiamGia FLOAT,
NgayBatDau DATE,
NgayKetThuc DATE
)
go

create table HoaDon(
MaHoaDon int identity(1, 1) primary key,
NgayLap DATETIME ,
TongTien Decimal(10,2),
ID_Person int,
FOREIGN KEY (ID_Person) REFERENCES dbo.Person(ID_Person),
)
go

create table ChiTietHoaDon(
MaHoaDon INT FOREIGN key (MaHoaDon) references HoaDon,
MaSach int foreign key (MaSach) references Sach,
SoLuong int,
MucGiamGia FLOAT,
TongTien Decimal(10,2),
constraint pk_ChiTiet primary key(MaHoaDon, MaSach)
)
go

create table LichSuThanhToan
(
ID_LichSuThanhToan int identity(1, 1) primary key,
ID_Person int,
FOREIGN KEY (ID_Person) REFERENCES dbo.Person(ID_Person),
MaHoaDon INT FOREIGN key (MaHoaDon) references HoaDon,
)
go

SELECT LichSuThanhToan.MaHoaDon
FROM LichSuThanhToan
WHERE ID_Person = 7


