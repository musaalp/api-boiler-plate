USE Albelli_Order;

INSERT INTO Products (`UnitType`, `UnitSize`) 
SELECT * FROM (SELECT 'PhotoBook', 19) as Tmp 
WHERE NOT EXISTS (
	SELECT UnitType FROM Products WHERE UnitType = 'PhotoBook'
) LIMIT 1;

INSERT INTO Products (`UnitType`, `UnitSize`) 
SELECT * FROM (SELECT 'Calendar', 10) as Tmp 
WHERE NOT EXISTS (
	SELECT UnitType FROM Products WHERE UnitType = 'Calendar'
) LIMIT 1;

INSERT INTO Products (`UnitType`, `UnitSize`) 
SELECT * FROM (SELECT 'Canvas', 16) as Tmp 
WHERE NOT EXISTS (
	SELECT UnitType FROM Products WHERE UnitType = 'Canvas'
) LIMIT 1;

INSERT INTO Products (`UnitType`, `UnitSize`) 
SELECT * FROM (SELECT 'Cards', 4.7) as Tmp 
WHERE NOT EXISTS (
	SELECT UnitType FROM Products WHERE UnitType = 'Cards'
) LIMIT 1;

INSERT INTO Products (`UnitType`, `UnitSize`) 
SELECT * FROM (SELECT 'Mug', 94) as Tmp 
WHERE NOT EXISTS (
	SELECT UnitType FROM Products WHERE UnitType = 'Mug'
) LIMIT 1;

SELECT * FROM Products;