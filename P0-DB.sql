--Drop Tables
DROP TABLE OrderItems;
DROP TABLE Orders;
DROP TABLE Users;
DROP TABLE Inventories;
DROP TABLE Items;
DROP TABLE Locations;



--Create Items Table
CREATE TABLE Items
(
    ItemID INT IDENTITY (1,1) PRIMARY KEY,
    ItemName VARCHAR(100) NOT NULL,
    ItemDescription NVARCHAR(512) NOT NULL,
    ItemPrice MONEY NOT NULL,
);

--Create Locations Table
CREATE TABLE Locations
(
    LocationID INT IDENTITY (1,1) PRIMARY KEY,
    LocationName NVARCHAR(100) NOT NULL,
    SalePercentage INT NOT NULL,
);

--Create Users Table
CREATE TABLE Users
(
    UserID INT IDENTITY (1,1) PRIMARY KEY,
    UserName NVARCHAR (200) UNIQUE NOT NULL,
    UserPassword NVARCHAR (32),
    LocationID INT FOREIGN KEY REFERENCES Locations(LocationID) NOT NULL,
    Manager INT,
);

--Create Inventory Table
CREATE TABLE Inventories
(
    InventoryID INT IDENTITY (1,1) PRIMARY KEY,
    ItemID INT FOREIGN KEY REFERENCES Items(ItemID) NOT NULL,
    LocationID INT FOREIGN KEY REFERENCES Locations(LocationID) NOt NULL,
    Quantity INT NOT NULL,
);

--Cretae Orders Table
CREATE TABLE Orders
(
    OrderID INT IDENTITY (1,1) PRIMARY KEY,
    OrderLocationID INT FOREIGN KEY REFERENCES Locations(LocationID) NOT NULL,
    OrderDate DATETIMEOFFSET NOT NULL,
    UserID INT FOREIGN KEY REFERENCES Users(UserID) NOT NULL,
    OrderTotal MONEY NOT NULL,
);

CREATE TABLE OrderItems
(
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID) NOT NULL,
    ItemID INT FOREIGN KEY REFERENCES Items(ItemID) NOT NULL,
    Quantity INT NOT NULL,
    Price MONEY NOT NULL,
)


--Insert data Items
INSERT Items
    (ItemName, ItemDescription, ItemPrice)
VALUES
    ('Small Monitor', 'A small monitor, supporting low resolution.', 74.99),
    ('Medium Monitor', 'A medium monitor, supporting mid range resolution.', 99.99),
    ('Large Monitor', 'A large monitor, with high resolution capabilities.', 149.99),
    ('baby-ATX', 'A baby-ATX motherboard, suitable for low performance systems', 99.99),
    ('mini-ATX', 'mini-ATX motherboard, capable of operatng an acerage workstation.', 124.99),
    ('ATX', 'A full ATX motherboard, designed for the peak of performance.', 174.99);



--Insert data Locations
INSERT Locations
    (LocationName, SalePercentage)
VALUES
    ('New York', 1),
    ('Dallas', 1),
    ('Miami', 1),
    ('Paris', 1),
    ('London', 1);

--Insert data Users
INSERT Users
    (UserName, UserPassword, LocationID, Manager)
VALUES
    ('Hawkkiller', 'password', 1, 1),
    ('Rachel', 'password', 2, 0),
    ('User1', 'password', 2, 0),
    ('Manager1', 'password', 3, 1);

--Insert data Inventories
INSERT Inventories
    (ItemID, LocationID, Quantity)
VALUES
    (1, 1, 4),
    (2, 1, 4),
    (3, 1, 4),
    (4, 1, 4),
    (5, 1, 4),
    (6, 1, 4),
    (1, 2, 4),
    (2, 2, 4),
    (3, 2, 4),
    (4, 2, 4),
    (5, 2, 4),
    (6, 2, 4),
    (1, 3, 4),
    (2, 3, 4),
    (3, 3, 4),
    (4, 3, 4),
    (5, 3, 4),
    (6, 3, 4),
    (1, 4, 4),
    (2, 4, 4),
    (3, 4, 4),
    (4, 4, 4),
    (5, 4, 4),
    (6, 4, 4),
    (1, 5, 4),
    (2, 5, 4),
    (3, 5, 4),
    (4, 5, 4),
    (5, 5, 4),
    (6, 5, 4);

--Insert data Orders
INSERT Orders
    (OrderDate, OrderLocationID, UserID, OrderTotal)
VALUES
    ('2022-01-19 12:35:29', 1, 2, 10),
    ('2022-01-19 12:35:29', 2, 2, 10),
    ('2022-01-19 12:35:29', 3, 2, 10),
    ('2022-01-19 12:35:29', 4, 2, 10),
    ('2022-01-19 12:35:29', 2, 3, 10),
    ('2022-01-19 12:35:29', 3, 3, 10),
    ('2022-01-19 12:35:29', 4, 3, 10),
    ('2022-01-19 12:35:29', 5, 3, 10),
    ('2022-01-19 12:35:29', 1, 3, 10);

--Insert data OrderItems
INSERT OrderItems
    (OrderID, ItemID, Quantity, Price)
VALUES
    (1, 1, 1, 5),
    (1, 2, 1, 5),
    (2, 2, 1, 5),
    (2, 3, 1, 5),
    (3, 3, 1, 5),
    (3, 4, 1, 5),
    (4, 4, 1, 5),
    (4, 5, 1, 5),
    (5, 1, 1, 5),
    (5, 2, 1, 5),
    (6, 1, 1, 5),
    (6, 2, 1, 5),
    (7, 3, 1, 5),
    (7, 5, 1, 5),
    (8, 1, 2, 5),
    (9, 4, 1, 5);


SELECT OrderItems.OrderID, Items.ItemID,Items.ItemName, OrderItems.Quantity, OrderItems.Price
FROM OrderItems
JOIN Items ON Items.ItemID = OrderItems.ItemID
WHERE OrderID = 2;

SELECT Inventories.InventoryID, Items.ItemID, Items.ItemPrice, Inventories.LocationID, Inventories.Quantity
FROM Inventories
JOIN Items ON Items.ItemID = Inventories.ItemID
WHERE LocationID = 2;

Select * from Inventories;

select Inventories.ItemID, Quantity, ItemName, ItemDescription, ItemPrice
from Inventories
join items on items.ItemID = Inventories.ItemID
where InventoryID = 1;

SELECT InventoryID, Inventories.ItemID, Quantity, ItemName, ItemDescription, ItemPrice
FROM Inventories
JOIN Items ON Items.ItemID = Inventories.ItemID
WHERE Inventories.ItemID = 2 AND Inventories.LocationID = 1;

SELECT UserName
FROM Users
WHERE UserID = 2;

SELECT *
FROM Inventories;

UPDATE Inventories
SET Quantity = 5
WHERE LocationID = 1 AND ItemId = 3;

SELECT *
FROM Inventories
WHERE ItemID = 7;

SELECT *
FROM Items;

UPDATE Locations
SET SalePercentage = 2
WHERE LocationID = 1;


SELECT Quantity
FROM Inventories
WHERE ItemID = 2 AND LocationID = 3;