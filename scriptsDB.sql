-- Crear la base de datos
CREATE DATABASE RigoRigo;
GO

-- Usar la base de datos
USE RigoRigo;
GO

-- Tabla de Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1, 1), -- Identificador único del cliente
    Cedula NVARCHAR(20) NOT NULL UNIQUE,     -- Cédula del cliente (única)
    Nombre NVARCHAR(100) NOT NULL,           -- Nombre del cliente
    Direccion NVARCHAR(200) NOT NULL         -- Dirección de entrega
);
GO

-- Tabla de Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1, 1), -- Identificador único del producto
    Nombre NVARCHAR(100) NOT NULL,             -- Nombre del producto
    Precio DECIMAL(18, 2) NOT NULL,            -- Precio del producto
    Stock INT NOT NULL                         -- Cantidad en stock
);
GO

-- Tabla de Pedidos
CREATE TABLE Pedidos (
    PedidoID INT PRIMARY KEY IDENTITY(1, 1),   -- Identificador único del pedido
    ClienteID INT NOT NULL,                    -- Cliente asociado al pedido
    Fecha DATETIME NOT NULL DEFAULT GETDATE(), -- Fecha del pedido
    ValorTotal DECIMAL(18, 2) NOT NULL,        -- Valor total del pedido
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID) -- Relación con Clientes
);
GO

-- Tabla de DetallePedido (relación muchos a muchos entre Pedidos y Productos)
CREATE TABLE DetallePedido (
    DetallePedidoID INT PRIMARY KEY IDENTITY(1, 1), -- Identificador único del detalle
    PedidoID INT NOT NULL,                          -- Pedido asociado
    ProductoID INT NOT NULL,                        -- Producto asociado
    Cantidad INT NOT NULL,                          -- Cantidad del producto
    PrecioUnitario DECIMAL(18, 2) NOT NULL,         -- Precio unitario al momento de la compra
    FOREIGN KEY (PedidoID) REFERENCES Pedidos(PedidoID), -- Relación con Pedidos
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID) -- Relación con Productos
);
GO

-- Procedimiento almacenado para crear un nuevo pedido
CREATE PROCEDURE CrearPedido
    @ClienteID INT,
    @ProductosDetalle NVARCHAR(MAX) -- JSON con los productos y cantidades
AS
BEGIN
    BEGIN TRANSACTION;

    DECLARE @ValorTotal DECIMAL(18, 2) = 0;
    DECLARE @PedidoID INT;

    -- Insertar el pedido
    INSERT INTO Pedidos (ClienteID, ValorTotal)
    VALUES (@ClienteID, 0);

    SET @PedidoID = SCOPE_IDENTITY();

    -- Procesar los productos del pedido
    DECLARE @ProductoID INT, @Cantidad INT, @Precio DECIMAL(18, 2);

    DECLARE productos_cursor CURSOR FOR
    SELECT ProductoID, Cantidad, Precio
    FROM OPENJSON(@ProductosDetalle)
    WITH (
        ProductoID INT '$.ProductoID',
        Cantidad INT '$.Cantidad',
        Precio DECIMAL(18, 2) '$.Precio'
    );

    OPEN productos_cursor;
    FETCH NEXT FROM productos_cursor INTO @ProductoID, @Cantidad, @Precio;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Insertar detalle del pedido
        INSERT INTO DetallePedido (PedidoID, ProductoID, Cantidad, PrecioUnitario)
        VALUES (@PedidoID, @ProductoID, @Cantidad, @Precio);

        -- Actualizar el valor total del pedido
        SET @ValorTotal = @ValorTotal + (@Cantidad * @Precio);

        -- Actualizar el stock del producto
        UPDATE Productos
        SET Stock = Stock - @Cantidad
        WHERE ProductoID = @ProductoID;

        FETCH NEXT FROM productos_cursor INTO @ProductoID, @Cantidad, @Precio;
    END;

    CLOSE productos_cursor;
    DEALLOCATE productos_cursor;

    -- Actualizar el valor total del pedido
    UPDATE Pedidos
    SET ValorTotal = @ValorTotal
    WHERE PedidoID = @PedidoID;

    COMMIT TRANSACTION;
END;
GO

-- Procedimiento almacenado para obtener todos los productos
CREATE PROCEDURE ObtenerProductos
AS
BEGIN
    SELECT ProductoID, Nombre, Precio, Stock
    FROM Productos;
END;
GO

-- Procedimiento almacenado para obtener los pedidos de un cliente
CREATE PROCEDURE ObtenerPedidosPorCliente
    @ClienteID INT
AS
BEGIN
    SELECT p.PedidoID, p.Fecha, p.ValorTotal, c.Nombre AS ClienteNombre
    FROM Pedidos p
    INNER JOIN Clientes c ON p.ClienteID = c.ClienteID
    WHERE p.ClienteID = @ClienteID;
END;
GO

-- Insertar datos de prueba
INSERT INTO Clientes (Cedula, Nombre, Direccion)
VALUES ('123456789', 'Juan Pérez', 'Calle 123, Ciudad');

INSERT INTO Productos (Nombre, Precio, Stock)
VALUES ('Bicicleta Montaña', 500.00, 10),
       ('Casco Deportivo', 50.00, 20),
       ('Guantes', 25.00, 30);

GO