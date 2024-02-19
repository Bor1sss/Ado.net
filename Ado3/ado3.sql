use ado3


CREATE TABLE ProductTypes (
    ProductTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL
);

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(255) NOT NULL,
    ProductTypeID INT,
    Quantity INT NOT NULL,
    CostPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ProductTypeID) REFERENCES ProductTypes(ProductTypeID)
);
CREATE TABLE SalesManagers (
    SalesManagerID INT IDENTITY(1,1) PRIMARY KEY,
    ManagerName VARCHAR(255) NOT NULL
);
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerCompanyName VARCHAR(255) NOT NULL
);

CREATE TABLE Sales (
    SaleID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT,
    SalesManagerID INT,
    CustomerID INT,

    QuantitySold INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    SaleDate DATE NOT NULL,

    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (SalesManagerID) REFERENCES SalesManagers(SalesManagerID),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);



-- ������� ������ � ������� ProductTypes
INSERT INTO ProductTypes (TypeName) VALUES 
('�����'), 
('��������'), 
('���������'), 
('�������'), 
('�������'), 
('����'), 
('��������'), 
('������������'), 
('�������'), 
('������');

-- ������� ������ � ������� Products
INSERT INTO Products (ProductName, ProductTypeID, Quantity, CostPrice) VALUES 
('����� �����', 1, 100, 0.50),
('������� ��������', 2, 50, 2.00),
('������ ���������', 3, 200, 0.75),
('��������� �������', 4, 500, 0.20),
('������ �������', 5, 150, 1.50),
('���� PVA', 6, 75, 3.50),
('�������� � ������� ��������', 7, 30, 5.00),
('������� ������������', 8, 20, 15.00),
('���������������� �������', 9, 40, 7.50),
('����� ������ A4', 10, 300, 0.10);

-- ������� ������ � ������� SalesManagers
INSERT INTO SalesManagers (ManagerName) VALUES 
('������'),
('������'),
('�������'),
('������'),
('��������'),
('�����'),
('�������'),
('�������'),
('�������'),
('�������');

-- ������� ������ � ������� Customers
INSERT INTO Customers (CustomerCompanyName) VALUES 
('��� "������1"'),
('�� "������2"'),
('��� "������3"'),
('�� "������4"'),
('��� "������5"'),
('�� "������6"'),
('��� "������7"'),
('�� "������8"'),
('��� "������9"'),
('�� "������10"');

-- ������� ������ � ������� Sales
INSERT INTO Sales (ProductID, SalesManagerID, CustomerID, QuantitySold, UnitPrice, SaleDate) VALUES 
(1, 1, 1, 50, 1.00, '2024-02-18'),
(2, 2, 2, 25, 3.00, '2024-02-19'),
(3, 3, 3, 100, 1.50, '2024-02-20'),
(4, 4, 4, 200, 0.10, '2024-02-21'),
(5, 5, 5, 75, 2.00, '2024-02-22'),
(6, 6, 6, 30, 4.00, '2024-02-23'),
(7, 7, 7, 10, 8.00, '2024-02-24'),
(8, 8, 8, 5, 20.00, '2024-02-25'),
(9, 9, 9, 15, 5.00, '2024-02-26'),
(10, 10, 10, 150, 0.50, '2024-02-27');



CREATE PROCEDURE GetAllInfo
AS
BEGIN
SELECT
    P.ProductID,
    P.ProductName,
    PT.TypeName AS ProductType,
    P.Quantity,
    P.CostPrice,
    SM.ManagerName AS SalesManager,
    C.CustomerCompanyName,
    S.QuantitySold,
    S.UnitPrice,
    S.SaleDate
FROM
    Products P
INNER JOIN
    ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
INNER JOIN
    Sales S ON P.ProductID = S.ProductID
INNER JOIN
    SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
INNER JOIN
    Customers C ON S.CustomerID = C.CustomerID;
END;

Create Procedure GetProductTypes
As
begin 
	Select TypeName From ProductTypes
End;

Create Procedure GetSalesManegers
As
begin 
	Select ManagerName From SalesManagers
End;

Create Procedure GetCustomers
As
begin 
	Select CustomerCompanyName From Customers
End;



--
CREATE PROCEDURE GetMaxQuantity
AS
BEGIN
    SELECT TOP 1
        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    ORDER BY
        P.Quantity DESC;
END;


CREATE PROCEDURE GetMinQuantity
AS
BEGIN
    SELECT TOP 1
        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    ORDER BY
        P.Quantity ASC;
END;




CREATE PROCEDURE GetMaxPrice
AS
BEGIN
    SELECT TOP 1
        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    ORDER BY
        P.CostPrice DESC;
END;





CREATE PROCEDURE GetProductsByType
    @ProductType VARCHAR(50)
AS
BEGIN
    SELECT
        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    WHERE
        PT.TypeName = @ProductType;
END;

CREATE PROCEDURE GetProductsBySalesManager
    @SalesManagerName VARCHAR(255)
AS
BEGIN
    SELECT
        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    WHERE
        SM.ManagerName = @SalesManagerName;
END;





CREATE PROCEDURE GetProductsByCustomerCompany
    @CustomerCompanyName VARCHAR(255)
AS
BEGIN
    SELECT
        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    WHERE
        C.CustomerCompanyName = @CustomerCompanyName;
END;

EXEC GetProductsByCustomerCompany @CustomerCompanyName = '��� "������1"';




CREATE PROCEDURE GetLatestSale
AS
BEGIN
    SELECT TOP 1
        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    ORDER BY
        S.SaleDate DESC;
END;


CREATE PROCEDURE GetAvgQ
AS
BEGIN
    SELECT
        PT.TypeName AS ProductType,
        AVG(P.Quantity) AS AverageQuantity
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    GROUP BY
        PT.TypeName;
END;

