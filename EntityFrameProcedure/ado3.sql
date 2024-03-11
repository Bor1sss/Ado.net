
create database ado4
GO
use ado4
GO
CREATE TABLE ProductTypes (
    ProductTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(255) NOT NULL,
    ProductTypeID INT,
    Quantity INT NOT NULL,
    CostPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ProductTypeID) REFERENCES ProductTypes(ProductTypeID)
);
GO
CREATE TABLE SalesManagers (
    SalesManagerID INT IDENTITY(1,1) PRIMARY KEY,
    ManagerName VARCHAR(255) NOT NULL
);
GO
CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerCompanyName VARCHAR(255) NOT NULL
);
GO
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
GO


-- Вставка данных в таблицу ProductTypes
INSERT INTO ProductTypes (TypeName) VALUES 
('Ручки'), 
('Блокноты'), 
('Карандаши'), 
('Скрепки'), 
('Ластики'), 
('Клей'), 
('Степлеры'), 
('Калькуляторы'), 
('Ножницы'), 
('Бумага');
GO
-- Вставка данных в таблицу Products
INSERT INTO Products (ProductName, ProductTypeID, Quantity, CostPrice) VALUES 
('Синие ручки', 1, 100, 0.50),
('Большие блокноты', 2, 50, 2.00),
('Черные карандаши', 3, 200, 0.75),
('Маленькие скрепки', 4, 500, 0.20),
('Мягкие ластики', 5, 150, 1.50),
('Клей PVA', 6, 75, 3.50),
('Степлеры с красным корпусом', 7, 30, 5.00),
('Научные калькуляторы', 8, 20, 15.00),
('Профессиональные ножницы', 9, 40, 7.50),
('Белая бумага A4', 10, 300, 0.10);
GO
-- Вставка данных в таблицу SalesManagers
INSERT INTO SalesManagers (ManagerName) VALUES 
('Иванов'),
('Петров'),
('Сидоров'),
('Козлов'),
('Васнецов'),
('Попов'),
('Лебедев'),
('Соколов'),
('Новиков'),
('Морозов');
GO
-- Вставка данных в таблицу Customers
INSERT INTO Customers (CustomerCompanyName) VALUES 
('ООО "Клиент1"'),
('ИП "Клиент2"'),
('ООО "Клиент3"'),
('ИП "Клиент4"'),
('ООО "Клиент5"'),
('ИП "Клиент6"'),
('ООО "Клиент7"'),
('ИП "Клиент8"'),
('ООО "Клиент9"'),
('ИП "Клиент10"');
GO
-- Вставка данных в таблицу Sales
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
GO
CREATE PROCEDURE GetAllInfo
AS
BEGIN
 SELECT

        P.ProductID,
        P.ProductName,
        PT.TypeName AS ProductType,
        P.Quantity,
        P.CostPrice,
		S.SaleID,
        SM.ManagerName AS SalesManager,
        C.CustomerCompanyName,
        S.QuantitySold,
        S.UnitPrice,
        S.SaleDate
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    LEFT JOIN
        Sales S ON P.ProductID = S.ProductID
    LEFT JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    LEFT JOIN
        Customers C ON S.CustomerID = C.CustomerID;
END;
GO

Create Procedure GetProductTypes
As
begin 
	Select * From ProductTypes
End;
GO

Create Procedure GetSalesManegers
As
begin 
	Select ManagerName From SalesManagers
End;
GO


Create Procedure GetCustomers
As
begin 
	Select CustomerCompanyName From Customers
End;
GO


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
GO

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
GO



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
GO




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
GO


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
GO




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
GO





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
GO

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
GO







CREATE PROCEDURE InsertNewProduct
    @ProductName VARCHAR(255),
    @ProductTypeName VARCHAR(50),
    @Quantity INT,
    @CostPrice DECIMAL(10, 2)
AS
BEGIN
    DECLARE @ProductTypeID INT;
    SELECT @ProductTypeID = ProductTypeID
    FROM ProductTypes
    WHERE TypeName = @ProductTypeName;
    IF @ProductTypeID IS NOT NULL
    BEGIN
       
        INSERT INTO Products (ProductName, ProductTypeID, Quantity, CostPrice)
        VALUES (@ProductName, @ProductTypeID, @Quantity, @CostPrice);
    END
    ELSE
    BEGIN
        PRINT 'Тип канцтовара не найден.';
    END
END;
GO


CREATE PROCEDURE InsertNewProductType
    @ProductTypeName VARCHAR(50)
AS
BEGIN
    INSERT INTO ProductTypes (TypeName)
    VALUES (@ProductTypeName);
END;
GO




CREATE PROCEDURE InsertNewSalesManager
    @ManagerName VARCHAR(255)
AS
BEGIN
    INSERT INTO SalesManagers (ManagerName)
    VALUES (@ManagerName);
END;
GO


CREATE PROCEDURE InsertNewCustomer
    @CustomerCompanyName VARCHAR(255)
AS
BEGIN
    INSERT INTO Customers (CustomerCompanyName)
    VALUES (@CustomerCompanyName);
END;
GO



CREATE PROCEDURE UpdateProductInfo
    @ProductID INT,
    @NewProductName VARCHAR(255),
    @NewProductTypeID INT,
    @NewQuantity INT,
    @NewCostPrice DECIMAL(10, 2)
AS
BEGIN
    UPDATE Products
    SET
        ProductName = @NewProductName,
        ProductTypeID = @NewProductTypeID,
        Quantity = @NewQuantity,
        CostPrice = @NewCostPrice
    WHERE
        ProductID = @ProductID;
END;
GO

CREATE PROCEDURE UpdateSalesData
    @SaleID INT,
    @ProductID INT,
    @SalesManagerID INT,
    @CustomerID INT,
    @QuantitySold INT,
    @UnitPrice DECIMAL(10, 2),
    @SaleDate DATE
AS
BEGIN
    UPDATE Sales
    SET
        ProductID = @ProductID,
        SalesManagerID = @SalesManagerID,
        CustomerID = @CustomerID,
        QuantitySold = @QuantitySold,
        UnitPrice = @UnitPrice,
        SaleDate = @SaleDate
    WHERE
        SaleID = @SaleID;
END;
GO


CREATE PROCEDURE ShowCustomerCompanies
AS
BEGIN
    SELECT * FROM Customers;
END;
GO

CREATE PROCEDURE UpdateCustomerCompany
    @CustomerID INT,
    @NewCompanyName VARCHAR(255)
AS
BEGIN
    -- Просто обновляем название фирмы покупателя
    UPDATE Customers
    SET CustomerCompanyName = @NewCompanyName
    WHERE CustomerID = @CustomerID;

  
END;
GO
CREATE PROCEDURE UpdateProductType
    @ProductTypeID INT,
    @NewProductTypeName VARCHAR(50)
AS
BEGIN
    -- Просто обновляем название типа канцтовара
    UPDATE ProductTypes
    SET TypeName = @NewProductTypeName
    WHERE ProductTypeID = @ProductTypeID;

END;
GO

CREATE PROCEDURE DeleteProductAndSales
    @ProductID INT
AS
BEGIN
     DELETE FROM Sales
    WHERE ProductID = @ProductID;

    DELETE FROM Products
    WHERE ProductID = @ProductID;

 

END;
GO


CREATE PROCEDURE DeleteSalesManager
    @SalesManagerID INT
AS
BEGIN
    -- Удаляем менеджера по продажам по указанному SalesManagerID
    DELETE FROM SalesManagers
    WHERE SalesManagerID = @SalesManagerID;

   
END;
GO
CREATE PROCEDURE DeleteCustomerCompany
    @CustomerID INT
AS
BEGIN
    -- Удаляем фирму покупателя по указанному CustomerID
    DELETE FROM Customers
    WHERE CustomerID = @CustomerID;

  
END;
GO


CREATE PROCEDURE DeleteProductType
    @ProductTypeID INT
AS
BEGIN
    -- Удаляем тип канцтовара по указанному ProductTypeID
    DELETE FROM ProductTypes
    WHERE ProductTypeID = @ProductTypeID;

  
END;
GO


CREATE PROCEDURE GetTopSalesManager
AS
BEGIN
    SELECT TOP 1
        SM.SalesManagerID,
        SM.ManagerName,
        SUM(S.QuantitySold) AS TotalQuantitySold
    FROM
        Sales S
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    GROUP BY
        SM.SalesManagerID, SM.ManagerName
    ORDER BY
        TotalQuantitySold DESC;
END;
GO


CREATE PROCEDURE GetTopProfitManager
AS
BEGIN
    SELECT TOP 1
        SM.SalesManagerID,
        SM.ManagerName,
        SUM(S.QuantitySold * S.UnitPrice) AS TotalProfit
    FROM
        Sales S
    INNER JOIN
        SalesManagers SM ON S.SalesManagerID = SM.SalesManagerID
    GROUP BY
        SM.SalesManagerID, SM.ManagerName
    ORDER BY
        TotalProfit DESC;
END;
GO


CREATE PROCEDURE GetTopCustomerByTotalAmount
AS
BEGIN
    SELECT TOP 1
        C.CustomerID,
        C.CustomerCompanyName,
        SUM(S.QuantitySold * S.UnitPrice) AS TotalAmount
    FROM
        Sales S
    INNER JOIN
        Customers C ON S.CustomerID = C.CustomerID
    GROUP BY
        C.CustomerID, C.CustomerCompanyName
    ORDER BY
        TotalAmount DESC;
END;
GO


CREATE PROCEDURE GetTopProductTypeByQuantitySold
AS
BEGIN
    SELECT TOP 1
        PT.ProductTypeID,
        PT.TypeName AS ProductType,
        SUM(S.QuantitySold) AS TotalQuantitySold
    FROM
        Products P
    INNER JOIN
        ProductTypes PT ON P.ProductTypeID = PT.ProductTypeID
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    GROUP BY
        PT.ProductTypeID, PT.TypeName
    ORDER BY
        TotalQuantitySold DESC;
END;
GO

CREATE PROCEDURE GetTopSellingProducts
AS
BEGIN
    SELECT TOP 1
        P.ProductName,
        SUM(S.QuantitySold) AS TotalQuantitySold
    FROM
        Products P
    INNER JOIN
        Sales S ON P.ProductID = S.ProductID
    GROUP BY
        P.ProductName
    ORDER BY
        TotalQuantitySold DESC;
END;
GO

CREATE PROCEDURE GetProductsNotSoldForDays
    @DaysThreshold INT
AS
BEGIN
    SELECT
        P.ProductName
    FROM
        Products P
    WHERE
        P.ProductID NOT IN (
            SELECT DISTINCT
                S.ProductID
            FROM
                Sales S
            WHERE
                DATEDIFF(DAY, S.SaleDate, GETDATE()) <= @DaysThreshold
        );
END;
GO