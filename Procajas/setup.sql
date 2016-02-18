IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = 'Procajas')
BEGIN
	CREATE DATABASE Procajas
END
ELSE
BEGIN
	RETURN
END
GO
;

-- ALMACEN
CREATE TABLE Procajas.dbo.Warehouse
(
	vc_material varchar(50) not null,
	vc_department varchar(3) not null,
	i_count int not null,
	dt_date datetime not null,
	vc_invoice varchar(50) null,
	vc_location varchar(10) null
);

CREATE NONCLUSTERED INDEX [ix_WarehouseMaterial] ON Procajas.dbo.Warehouse
(
	vc_material ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_WarehouseDepartment] ON Procajas.dbo.Warehouse
(
	vc_department ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_WarehouseDate] ON Procajas.dbo.Warehouse
(
	dt_date ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_WarehouseInvoice] ON Procajas.dbo.Warehouse
(
	vc_invoice ASC
) ON [PRIMARY]
GO

-- PROCESO
CREATE TABLE Procajas.dbo.Process
(
	Material varchar(50) not null,
	Departamento varchar(3) not null,
	Ubicacion varchar(10) not null,
	Fecha datetime not null,
	Detalles varchar(max) null
);

CREATE NONCLUSTERED INDEX [ix_ProcesoMaterial] ON Procajas.dbo.Proceso
(
	[Material] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProcesoDepartamento] ON Procajas.dbo.Proceso
(
	[Departamento] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProcesoFecha] ON Procajas.dbo.Proceso
(
	[Fecha] ASC
) ON [PRIMARY]
GO


-- PRODUCTOTERMINADO
CREATE TABLE Procajas.dbo.ProductoTerminado
(
	Material varchar(50) not null,
	Cantidad int not null,
	Fecha datetime not null,
	Factura varchar(50) null,
	Ubicacion varchar(10) null
);

CREATE NONCLUSTERED INDEX [ix_ProductoTerminadoMaterial] ON Procajas.dbo.ProductoTerminado
(
	[Material] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProductoTerminadoFecha] ON Procajas.dbo.ProductoTerminado
(
	[Fecha] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProductoTerminadoFactura] ON Procajas.dbo.ProductoTerminado
(
	[Factura] ASC
) ON [PRIMARY]
GO


-- DISCREPANCIAS
CREATE TABLE Procajas.dbo.Discrepancias
(
	Material varchar(50) not null,
	Departamento varchar(3) not null,
	Cantidad int not null,
	Fecha datetime not null,
	Trabajo varchar(50) not null
);

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasMaterial] ON Procajas.dbo.Discrepancias
(
	[Material] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasDepartamento] ON Procajas.dbo.Discrepancias
(
	[Departamento] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasTrabajo] ON Procajas.dbo.Discrepancias
(
	[Trabajo] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasFecha] ON Procajas.dbo.Discrepancias
(
	[Fecha] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasFactura] ON Procajas.dbo.Discrepancias
(
	[Factura] ASC
) ON [PRIMARY]
GO


finish:
;