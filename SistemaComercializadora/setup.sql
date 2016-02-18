IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = 'SistemaComercializadora')
BEGIN
	CREATE DATABASE SistemaComercializadora
END
ELSE
BEGIN
	RETURN
END
GO
;

-- ALMACEN
CREATE TABLE SistemaComercializadora.dbo.Almacen
(
	Material varchar(50) not null,
	Departamento varchar(3) not null,
	Cantidad int not null,
	Fecha datetime not null,
	Factura varchar(50) null,
	Ubicacion varchar(10) null
);

CREATE NONCLUSTERED INDEX [ix_AlmacenMaterial] ON SistemaComercializadora.dbo.Almacen
(
	[Material] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_AlmacenDepartamento] ON SistemaComercializadora.dbo.Almacen
(
	[Departamento] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_AlmacenFecha] ON SistemaComercializadora.dbo.Almacen
(
	[Fecha] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_AlmacenFactura] ON SistemaComercializadora.dbo.Almacen
(
	[Factura] ASC
) ON [PRIMARY]
GO

-- PROCESO
CREATE TABLE SistemaComercializadora.dbo.Proceso
(
	Material varchar(50) not null,
	Departamento varchar(3) not null,
	Ubicacion varchar(10) not null,
	Fecha datetime not null,
	Detalles varchar(max) null
);

CREATE NONCLUSTERED INDEX [ix_ProcesoMaterial] ON SistemaComercializadora.dbo.Proceso
(
	[Material] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProcesoDepartamento] ON SistemaComercializadora.dbo.Proceso
(
	[Departamento] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProcesoFecha] ON SistemaComercializadora.dbo.Proceso
(
	[Fecha] ASC
) ON [PRIMARY]
GO


-- PRODUCTOTERMINADO
CREATE TABLE SistemaComercializadora.dbo.ProductoTerminado
(
	Material varchar(50) not null,
	Cantidad int not null,
	Fecha datetime not null,
	Factura varchar(50) null,
	Ubicacion varchar(10) null
);

CREATE NONCLUSTERED INDEX [ix_ProductoTerminadoMaterial] ON SistemaComercializadora.dbo.ProductoTerminado
(
	[Material] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProductoTerminadoFecha] ON SistemaComercializadora.dbo.ProductoTerminado
(
	[Fecha] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProductoTerminadoFactura] ON SistemaComercializadora.dbo.ProductoTerminado
(
	[Factura] ASC
) ON [PRIMARY]
GO


-- DISCREPANCIAS
CREATE TABLE SistemaComercializadora.dbo.Discrepancias
(
	Material varchar(50) not null,
	Departamento varchar(3) not null,
	Cantidad int not null,
	Fecha datetime not null,
	Trabajo varchar(50) not null
);

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasMaterial] ON SistemaComercializadora.dbo.Discrepancias
(
	[Material] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasDepartamento] ON SistemaComercializadora.dbo.Discrepancias
(
	[Departamento] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasTrabajo] ON SistemaComercializadora.dbo.Discrepancias
(
	[Trabajo] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasFecha] ON SistemaComercializadora.dbo.Discrepancias
(
	[Fecha] ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciasFactura] ON SistemaComercializadora.dbo.Discrepancias
(
	[Factura] ASC
) ON [PRIMARY]
GO


finish:
;