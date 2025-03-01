
SELECT 
	NAME AS LoginName, 
	TYPE_DESC AS AccountType, 
	create_date, 
	modify_date,
	TYPE
FROM sys.server_principals
WHERE TYPE IN ('S', 'U', 'G');
GO

CREATE LOGIN testing WITH PASSWORD = 'testing', CHECK_POLICY = OFF;
GO
CREATE DATABASE db_testing
GO
USE db_testing
GO
CREATE USER testing FOR LOGIN testing;
GO
EXEC sp_addrolemember 'db_owner', 'testing';

USE db_testing

-- Tables -------------------------------------------------------
-- --------------------------------------------------------------

CREATE TABLE Mae_Config (
	Id INT PRIMARY KEY IDENTITY(1,1),
	ApiKey varchar(256) NOT NULL,
	IsEnableRegister BIT NOT NULL,
)
GO

CREATE TABLE Auth_Profiles (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(50) NOT NULL,
	UNIQUE(Name),
) 
GO

CREATE TABLE Auth_Users (
	Id VARCHAR(256) PRIMARY KEY,
	Email VARCHAR(100) NOT NULL,
	HashLogin VARCHAR(256) NOT NULL,
	SaltLogin VARCHAR(256) NOT NULL,
	HashPM VARCHAR(256),
	SaltPM VARCHAR(256),
	SqlToken VARCHAR(256) ,
	IdProfile INT NOT NULL
	UNIQUE(Email),
	FOREIGN KEY (IdProfile) REFERENCES Auth_Profiles(Id)
) 
GO

CREATE TABLE PM_Core (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Data01 VARCHAR(256) NOT NULL,
	Data02 VARCHAR(256) NOT NULL,
	Data03 VARCHAR(256) NOT NULL,
	IdUser VARCHAR(256) NOT NULL
	FOREIGN KEY (IdUser) REFERENCES Auth_Users(Id)
)
GO

DROP TABLE Mae_Config
GO
DROP TABLE Auth_Profiles
GO
DROP TABLE Auth_Users
GO
DROP TABLE PM_Core
GO

-- Data ---------------------------------------------------------
-- --------------------------------------------------------------

SET IDENTITY_INSERT Auth_Profiles ON
GO
INSERT INTO Auth_Profiles
	(Id, Name)
VALUES
	(1, 'ADMIN'),
	(2, 'USER')
SET IDENTITY_INSERT Auth_Profiles OFF
GO

-- Stored Procedure ---------------------------------------------
-- --------------------------------------------------------------

-- Query --------------------------------------------------------
-- --------------------------------------------------------------

SELECT * FROM PM_Core
SELECT * FROM Mae_Config
SELECT * FROM Auth_Profiles
SELECT * FROM Auth_Users

SELECT
	a.Id,
	a.Email,
	a.HashLogin,
	a.SaltLogin,
	a.HashPM,
	a.SaltPM,
	a.SqlToken,
	b.Name AS Role
FROM Auth_Users a
	INNER JOIN Auth_Profiles b ON a.IdProfile = b.Id

SELECT
	Id, Data01, Data02, Data03, IdUser 
FROM 
	PM_Core 
WHERE 
	IdUser = 'cddf8d84-37d4-4c53-be39-53c95cb10a65'

-- --------------------------------------------------------------
-- --------------------------------------------------------------
