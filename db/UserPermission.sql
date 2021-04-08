DROP SCHEMA IF EXISTS `Order`;
CREATE SCHEMA `Order`;

USE `Order`;

CREATE TABLE Users (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,  
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE Permissions (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Value` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE UserPermissions (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `PermissionId` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO Users (`UserName`, `Email`, `FirstName`,`LastName`) 
SELECT * FROM (SELECT 'musa.alp', 'musa.alp.dev@gmail.com','Musa','Alp') as Tmp 
WHERE NOT EXISTS (
	SELECT UserName FROM Users WHERE UserName = 'musa.alp'
) LIMIT 1;

INSERT INTO Permissions (`Name`, `Value`) 
SELECT * FROM (SELECT 'Admin', 'admin:*') as Tmp 
WHERE NOT EXISTS (
	SELECT Name FROM Permissions WHERE Name = 'Admin'
) LIMIT 1;