CREATE DATABASE ORION_JOFREY;
GO

USE ORION_JOFREY;
GO



CREATE TABLE Customers(
	Id INT IDENTITY(1,1) Primary key,
	[Name] varchar(300),
	[Description] varchar(600),
	CreatedAt datetime not null default(GETDATE()),
	UpdatedAt datetime not null default(GETDATE()),
);

CREATE TABLE CustomerAddresses(
	Id INT IDENTITY(1,1) Primary key,
	CustomerId INT Foreign Key references Customers(Id),
	[Name] varchar(100),
	Street	varchar(200),
	PostalCode varchar(50),
	City varchar(200),
	Country varchar(200),
	CreatedAt datetime not null default(GETDATE()),
	UpdatedAt datetime not null default(GETDATE())
);

