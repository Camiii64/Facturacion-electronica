CREATE DATABASE DTE_DB;
GO
USE DTE_DB;
GO

-- ===============================================
-- Tabla: Emisor
-- ===============================================
CREATE TABLE Emisor (
    EmisorId INT IDENTITY(1,1) PRIMARY KEY,
    NIT NVARCHAR(17) NOT NULL,
    NRC NVARCHAR(8) NOT NULL,
    Nombre NVARCHAR(250) NOT NULL,
    Direccion NVARCHAR(300),
    Telefono NVARCHAR(30),
    Email NVARCHAR(100)
);

-- ===============================================
-- Tabla: Cliente
-- ===============================================
CREATE TABLE Cliente (
    ClienteId INT IDENTITY(1,1) PRIMARY KEY,
    NIT NVARCHAR(17) NOT NULL,
    NRC NVARCHAR(8),
    Nombre NVARCHAR(200) NOT NULL,
    Direccion NVARCHAR(300),
    Telefono NVARCHAR(30),
    Email NVARCHAR(100)
);

-- ===============================================
-- Tabla: CategoriaProducto
-- ===============================================
CREATE TABLE CategoriaProducto (
    CategoriaId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(300)
);

-- ===============================================
-- Tabla: Producto
-- ===============================================
CREATE TABLE Producto (
    ProductoId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Descripcion NVARCHAR(500),
    Codigo NVARCHAR(50),
    Precio DECIMAL(18,2) NOT NULL,
    CategoriaId INT NULL,
	Existencias INT NOT NULL,
    TipoItem INT DEFAULT 1, -- 1 = Bien, 2 = Servicio
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (CategoriaId)
        REFERENCES CategoriaProducto(CategoriaId)
);

-- ===============================================
-- Tabla: Document
-- ===============================================
CREATE TABLE Document (
    DocumentId INT IDENTITY(1,1) PRIMARY KEY,
    EmisorId INT NOT NULL,
    ClienteId INT NULL,
    NumeroConsecutivo NVARCHAR(50) NOT NULL,
    NumeroFactura NVARCHAR(50),
    FechaEmision DATETIME NOT NULL,
    TipoDocumento NVARCHAR(50),
    Moneda NVARCHAR(10),
    Estado NVARCHAR(50),
    Total DECIMAL(18,2) DEFAULT 0,
    Observaciones NVARCHAR(500),
    CONSTRAINT FK_Document_Emisor FOREIGN KEY (EmisorId)
        REFERENCES Emisor(EmisorId),
    CONSTRAINT FK_Document_Cliente FOREIGN KEY (ClienteId)
        REFERENCES Cliente(ClienteId)
);

-- ===============================================
-- Tabla: DetalleDocumento
-- ===============================================
CREATE TABLE DetalleDocumento (
    DetalleId INT IDENTITY(1,1) PRIMARY KEY,
    DocumentId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad DECIMAL(18,2) NOT NULL DEFAULT 1,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    SubTotal AS (Cantidad * PrecioUnitario),
    CONSTRAINT FK_DetalleDocumento_Document FOREIGN KEY (DocumentId)
        REFERENCES Document(DocumentId),
    CONSTRAINT FK_DetalleDocumento_Producto FOREIGN KEY (ProductoId)
        REFERENCES Producto(ProductoId)
);

-- ===============================================
-- Tabla: Resumen
-- ===============================================
CREATE TABLE Resumen (
    ResumenId INT IDENTITY(1,1) PRIMARY KEY,
    DocumentId INT NOT NULL,
    MontoExento DECIMAL(18,2) DEFAULT 0,
    MontoGrabado DECIMAL(18,2) DEFAULT 0,
    MontoImpuestos DECIMAL(18,2) DEFAULT 0,
    TotalVenta DECIMAL(18,2) DEFAULT 0,
    TotalPagado DECIMAL(18,2) DEFAULT 0,
    CONSTRAINT FK_Resumen_Document FOREIGN KEY (DocumentId)
        REFERENCES Document(DocumentId)
);

-- ===============================================
-- Tabla: MedioPago
-- ===============================================
CREATE TABLE MedioPago (
    MedioPagoId INT IDENTITY(1,1) PRIMARY KEY,
    DocumentId INT NOT NULL,
    CodigoMedio NVARCHAR(50),
    Descripcion NVARCHAR(200),
    Monto DECIMAL(18,2) DEFAULT 0,
    Banco NVARCHAR(200),
    NumeroOperacion NVARCHAR(200),
    CONSTRAINT FK_MedioPago_Document FOREIGN KEY (DocumentId)
        REFERENCES Document(DocumentId)
);

-- ===============================================
-- Tabla: Extension (adendas / extensiones)
-- ===============================================
CREATE TABLE Extension (
    ExtensionId INT IDENTITY(1,1) PRIMARY KEY,
    DocumentId INT NOT NULL,
    Nombre NVARCHAR(200),
    Valor NVARCHAR(MAX),
    CONSTRAINT FK_Extension_Document FOREIGN KEY (DocumentId)
        REFERENCES Document(DocumentId)
);

-- ===============================================
-- Tabla: Mensaje
-- ===============================================
CREATE TABLE Mensaje (
    MensajeId INT IDENTITY(1,1) PRIMARY KEY,
    DocumentId INT NOT NULL,
    Codigo NVARCHAR(50),
    Texto NVARCHAR(1000),
    CONSTRAINT FK_Mensaje_Document FOREIGN KEY (DocumentId)
        REFERENCES Document(DocumentId)
);

-- ===============================================
-- Tabla: Firma
-- ===============================================
CREATE TABLE Firma (
    FirmaId INT IDENTITY(1,1) PRIMARY KEY,
    DocumentId INT NOT NULL,
    FechaFirma DATETIME,
    FirmaXML NVARCHAR(MAX),
    Certificado NVARCHAR(MAX),
    CONSTRAINT FK_Firma_Document FOREIGN KEY (DocumentId)
        REFERENCES Document(DocumentId)
);

-- ===============================================
-- Tabla: Impuesto
-- ===============================================
CREATE TABLE Impuesto (
    ImpuestoId INT IDENTITY(1,1) PRIMARY KEY,
    DetalleId INT NOT NULL,
    Tipo NVARCHAR(100) NOT NULL,
    Tasa DECIMAL(5,2) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Impuesto_Detalle FOREIGN KEY (DetalleId)
        REFERENCES DetalleDocumento(DetalleId)
);

-- ===============================================
-- Tabla: Usuario
-- ===============================================
CREATE TABLE Usuario (
    UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario NVARCHAR(100) NOT NULL UNIQUE,
    ClaveHash NVARCHAR(500) NOT NULL,
    Rol NVARCHAR(50) DEFAULT 'Empleado',
    Estado BIT DEFAULT 1
);

-- ===============================================
-- Tabla: Bitacora
-- ===============================================
CREATE TABLE Bitacora (
    BitacoraId INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId INT,
    Fecha DATETIME DEFAULT GETDATE(),
    Accion NVARCHAR(300),
    Detalle NVARCHAR(MAX),
    CONSTRAINT FK_Bitacora_Usuario FOREIGN KEY (UsuarioId)
        REFERENCES Usuario(UsuarioId)
);

-- ===============================================
-- Índices sugeridos
-- ===============================================
CREATE INDEX IDX_Document_Numero ON Document(NumeroConsecutivo);
CREATE INDEX IDX_Document_Fecha ON Document(FechaEmision);
CREATE INDEX IDX_Emisor_NIT ON Emisor(NIT);
CREATE INDEX IDX_Cliente_NIT ON Cliente(NIT);
CREATE INDEX IDX_Producto_Codigo ON Producto(Codigo);
CREATE INDEX IDX_Detalle_DocumentId ON DetalleDocumento(DocumentId);
CREATE INDEX IDX_Document_ClienteId ON Document(ClienteId);
GO