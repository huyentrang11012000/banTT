use master
go

create database QuanLyDiem
go

use QuanLyDiem
go

create table DanToc(
	MaDanToc varchar(10) primary key,
	TenDanToc nvarchar(50) not null
)

create table HeDaoTao(
	MaHDT varchar(10) primary key,
	TenHDT nvarchar(50) not null
)

create table Khoa(
	MaKhoa varchar(10) primary key ,
	TenKhoa nvarchar(50),
	DienThoai varchar(15),
	DiaChi nvarchar(50),
	Website varchar(100)
)

create table MonHoc(
	MaMon varchar(10) primary key,
	TenMon nvarchar(50) not null,
	DVHT int not null 
)

create table ChucVu(
	MaChucVu varchar(10) primary key,
	TenChucVu nvarchar(50) not null
)


create table PhongHoc(
	MaPhong varchar(10) primary key,
	TenPhong nvarchar(50) not null
)

create table Que(
	MaQue varchar(10)primary key,
	TenQue nvarchar(50) not null
)

create table ChuyenNganh(
	MaChuyenNganh varchar(10) primary key,
	TenChuyenNganh nvarchar(50) not null,
	MaKhoa varchar(10) not null,
	constraint mk_KCN_fk  foreign key (MaKhoa) references Khoa(MaKhoa) 
)
	
create table Lop(
	MaLop varchar(10) primary key ,
	TenLop nvarchar(50) not null,
	MaKhoa varchar(10) not null,
	KhoaHoc varchar(10) not null,
	SiSo int not null,
	constraint mk_lop_fk foreign key (MaKhoa) references Khoa(MaKhoa) 
)

create table Thoi_Khoa_Bieu(
    id int identity(1,1) primary key,
	MaLop varchar(10),
	MaMon varchar(10),
	HocKy int not null,
	ThuHoc nvarchar(50) not null,
	CaHoc int not null,
	MaPhong varchar(10),
	constraint mp_TKB_fk  foreign key (MaPhong) references PhongHoc(MaPhong),
	constraint ml_TKB_fk  foreign key (MaLop) references Lop(MaLop),
	constraint mm_TKB_fk  foreign key (MaMon) references MonHoc(MaMon) 
)
create table SinhVien(
	MaSV varchar(15) primary key,
	TenSV nvarchar(50) not null,
	MaKhoa varchar(10) not null,
	MaLop varchar(10) not null,
	NgaySinh datetime not null,
	GioiTinh nvarchar(5),
	MaQue varchar(10) not null,
	MaDanToc varchar(10) not null,
	MaChuyenNganh varchar(10) not null,
	MaHDT varchar(10) not null,
	MaChucVu varchar(10) not null,
	constraint mk_SV_fk foreign key (MaKhoa) references Khoa(MaKhoa)  ,
	constraint ml_SV_fk foreign key (MaLop) references Lop(MaLop),
	constraint mq_SV_fk foreign key (MaQue) references Que(MaQue) ,
	constraint mdt_SV_fk foreign key (MaDanToc) references DanToc(MaDanToc) ,
	constraint mcn_SV_fk foreign key (MaChuyenNganh) references ChuyenNganh(MaChuyenNganh) ,
	constraint mhdt_SV_fk foreign key (MaHDT) references HeDaoTao(MaHDT) ,
	constraint mcv_SV_fk foreign key (MaChucVu) references ChucVu(MaChucVu) 
)

create table Diem(
	MaSV varchar(15) not null,
	MaLop varchar(10) not null,
	MaMon varchar(10) not null,
	HocKy int not null,
	LanThi int not null,
	Diem float ,
	constraint sv_Diem_fk  foreign key (MaSV) references SinhVien(MaSV) ,
	constraint lop_Diem_fk  foreign key (MaLop) references Lop(MaLop) ,
	constraint mon_Diem_fk  foreign key (MaMon) references MonHoc(MaMon) 
)
