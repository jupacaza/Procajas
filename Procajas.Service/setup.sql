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
CREATE TABLE dbo.Warehouse
(
	vc_material varchar(50) not null,
	vc_department varchar(3) not null,
	i_count int not null,
	dt_date datetime not null,
	vc_invoice varchar(50) null,
	vc_location varchar(10) null
);
GO

CREATE NONCLUSTERED INDEX [ix_WarehouseMaterial] ON dbo.Warehouse
(
	vc_material ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_WarehouseDepartment] ON dbo.Warehouse
(
	vc_department ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_WarehouseDate] ON dbo.Warehouse
(
	dt_date ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_WarehouseInvoice] ON dbo.Warehouse
(
	vc_invoice ASC
) ON [PRIMARY]
GO

-- PROCESO
CREATE TABLE dbo.Process
(
	vc_material varchar(50) not null,
	vc_department varchar(3) not null,
	vc_location varchar(10) not null,
	dt_date datetime not null,
	vc_details varchar(max) null
);
GO

CREATE NONCLUSTERED INDEX [ix_ProcessMaterial] ON dbo.Process
(
	vc_material ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProcessDepartment] ON dbo.Process
(
	vc_department ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_ProcessDate] ON dbo.Process
(
	dt_date ASC
) ON [PRIMARY]
GO


-- PRODUCTOTERMINADO
CREATE TABLE dbo.FinishedProduct
(
	vc_material varchar(50) not null,
	i_count int not null,
	dt_date datetime not null,
	vc_invoice varchar(50) null,
	vc_location varchar(10) null
);
GO

CREATE NONCLUSTERED INDEX [ix_FinishedProductMaterial] ON dbo.FinishedProduct
(
	vc_material ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_FinishedProductDate] ON dbo.FinishedProduct
(
	dt_date ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_FinishedProductInvoice] ON dbo.FinishedProduct
(
	vc_invoice ASC
) ON [PRIMARY]
GO


-- DISCREPANCIAS
CREATE TABLE dbo.Discrepancies
(
	vc_material varchar(50) not null,
	vc_department varchar(3) not null,
	i_count int not null,
	dt_date datetime not null,
	vc_job varchar(50) not null
);
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciesMaterial] ON dbo.Discrepancies
(
	vc_material ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciesDepartment] ON dbo.Discrepancies
(
	vc_department ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciesJob] ON dbo.Discrepancies
(
	vc_job ASC
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [ix_DiscrepanciesDate] ON dbo.Discrepancies
(
	dt_date ASC
) ON [PRIMARY]
GO
