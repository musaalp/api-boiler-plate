DROP SCHEMA IF EXISTS Albelli_Order;
CREATE SCHEMA Albelli_Order;

USE Albelli_Order;

CREATE TABLE Products (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UnitType` varchar(100) NOT NULL,
  `UnitSize` decimal(6,2) NOT NULL,  
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE Orders (
  `Id` int NOT NULL AUTO_INCREMENT,  
  `OrderId` varchar(50) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `MinBinWidth` decimal(6,2) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE OrderDetails (
  `Id` int NOT NULL AUTO_INCREMENT,  
  `OrderId` int NOT NULL,
  `ProductId` int NOT NULL,  
  `Quantity` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
