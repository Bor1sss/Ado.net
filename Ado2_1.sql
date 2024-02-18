Create DataBase Ado2_1
use Ado2_1
drop database Ado2_1

CREATE TABLE Product_Types (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Product_Type VARCHAR(50) NOT NULL
);

CREATE TABLE Suppliers (
    Supplier_ID INT PRIMARY KEY IDENTITY(1,1),
    Supplier_Name VARCHAR(100) NOT NULL,
    Supplier_Address VARCHAR(255)
);

CREATE TABLE Products (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Product_Name VARCHAR(255) NOT NULL,
    Product_Type_ID INT,
    Supplier_ID INT NOT NULL,
    FOREIGN KEY (Product_Type_ID) REFERENCES Product_Types(ID),
    FOREIGN KEY (Supplier_ID) REFERENCES Suppliers(Supplier_ID)
);

CREATE TABLE Deliveries (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Product_ID INT,
    Quantity INT,
    Cost DECIMAL(10, 2),
    Delivery_Date DATE,
    FOREIGN KEY (Product_ID) REFERENCES Products(ID)
);

-- Remove explicit ID values in the INSERT statements for Product_Types and Suppliers
INSERT INTO Product_Types (Product_Type) VALUES
('Electronics'),
('Clothing'),
('Home and Kitchen'),
('Books'),
('Beauty'),
('Toys'),
('Sports and Outdoors'),
('Furniture'),
('Automotive'),
('Jewelry');

INSERT INTO Suppliers (Supplier_Name, Supplier_Address) VALUES
('ABC Electronics Supplier', '123 Main Street, Cityville'),
('Fashion Haven', '456 Fashion Avenue, Style City'),
('Home Essentials Co.', '789 Homestead Lane, Decoratown'),
('Books Unlimited', '101 Library Street, Booksville'),
('Beauty Emporium', '234 Glamour Road, Beautopia'),
('Toy Galaxy', '567 Play Street, Kidstown'),
('Sports World', '890 Recreation Lane, Sportsville'),
('Furniture Hub', '111 Comfort Avenue, Furnitropolis'),
('Auto Parts Plus', '222 Mechanic Road, Autotown'),
('JewelCraft', '333 Gemstone Lane, Gem City');

-- Remove explicit ID values in the INSERT statements for Products and Deliveries
INSERT INTO Products (Product_Name, Product_Type_ID, Supplier_ID) VALUES
('Smartphone X', 1, 1),
('Mens T-Shirt', 2, 2),
('Kitchen Blender', 3, 3),
('Science Fiction Book', 4, 4),
('Lipstick Red', 5, 5),
('Toy Robot', 6, 6),
('Soccer Ball', 7, 7),
('Leather Sofa', 8, 8),
('Car Battery', 9, 9),
('Diamond Necklace', 10, 10);

INSERT INTO Deliveries (Product_ID, Quantity, Cost, Delivery_Date) VALUES
(1, 100, 5000.00, '2024-02-01'),
(2, 50, 750.00, '2024-02-02'),
(3, 20, 120.00, '2024-02-03'),
(4, 30, 450.00, '2024-02-04'),
(5, 40, 800.00, '2024-02-05'),
(6, 10, 150.00, '2024-02-06'),
(7, 25, 300.00, '2024-02-07'),
(8, 5, 1200.00, '2024-02-08'),
(9, 15, 250.00, '2024-02-09'),
(10, 2, 5000.00, '2024-02-10');


UPDATE Products
SET Product_Name = 'Latest Smartphone',
    Supplier_ID = 2
WHERE ID = 1;



