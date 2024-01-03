IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Pharmacy] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Address] nvarchar(250) NOT NULL,
    [City] nvarchar(50) NOT NULL,
    [State] nvarchar(50) NOT NULL,
    [Zip] int NOT NULL,
    [FilledPrescriptionsCount] int NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_Pharmacy] PRIMARY KEY ([Id])
);
GO

    SET IDENTITY_INSERT Pharmacy ON;
    INSERT INTO Pharmacy (Id, Name, Address, City, State, Zip, FilledPrescriptionsCount, CreatedDate)
    VALUES
    (1, 'Bill''s Pharmacy', '123 Main St.', 'Dallas', 'TX', 75201, 0, GETDATE()),
    (2, 'Barbs''s Pharmacy', '123 Main St.', 'Plano', 'TX', 75074, 0, GETDATE()),
    (3, 'House O Pills', '123 Main St.', 'Irving', 'TX', 75014, 0, GETDATE()),
    (4, 'RX R US', '123 Main St.', 'Ft Worth', 'TX', 75050, 0, GETDATE()),
    (5, 'Los Druggos', '123 Main St.', 'Arlington', 'TX', 76001, 0, GETDATE())
    SET IDENTITY_INSERT Pharmacy OFF;
            
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231215170339_Initial', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Pharmacy] ADD [CreatedBy] nvarchar(50) NOT NULL DEFAULT N'HelloCode';
GO

ALTER TABLE [Pharmacy] ADD [UpdatedBy] nvarchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231220192019_UddedCreatedByAndUpdatedByToPharmacy', N'7.0.14');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Pharmacist] (
    [Id] int NOT NULL IDENTITY,
    [PharmacyId] int NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Age] int NOT NULL,
    [PrimaryDrugSold] nvarchar(150) NOT NULL,
    [DateOfHire] datetime2 NOT NULL,
    CONSTRAINT [PK_Pharmacist] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pharmacist_Pharmacy_PharmacyId] FOREIGN KEY ([PharmacyId]) REFERENCES [Pharmacy] ([Id])
);
GO

CREATE TABLE [Warehouse] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(50) NOT NULL,
    [Address] nvarchar(250) NOT NULL,
    [City] nvarchar(50) NOT NULL,
    [State] nvarchar(50) NOT NULL,
    [Zip] int NOT NULL,
    CONSTRAINT [PK_Warehouse] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Delivery] (
    [Id] int NOT NULL IDENTITY,
    [PharmacyId] int NOT NULL,
    [WarehouseId] int NOT NULL,
    [DrugName] nvarchar(50) NOT NULL,
    [UnitCount] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [DeliveryDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Delivery] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Delivery_Pharmacy_PharmacyId] FOREIGN KEY ([PharmacyId]) REFERENCES [Pharmacy] ([Id]),
    CONSTRAINT [FK_Delivery_Warehouse_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouse] ([Id])
);
GO


SET IDENTITY_INSERT Pharmacist ON;
INSERT INTO Pharmacist (Id, PharmacyId, FirstName, LastName, Age, PrimaryDrugSold, DateOfHire)
VALUES
(1, 1, 'Bill', 'Jones', 55, 'Ibuprofen', '4-25-2003'),
(2, 1, 'Bill', 'Smith', 35, 'Pepcid', '4-25-2013'),
(3, 2, 'Barb', 'Jones', 53, 'Amitriptyline', '11-01-2001'),
(4, 2, 'Barb', 'Smith', 22, 'Asprin', '5-26-2012'),
(5, 3, 'Bill', 'House', 45, 'Tylenol', '6-27-2011'),
(6, 3, 'Barb', 'House', 35, 'Sudafed', '7-28-2010'),
(7, 4, 'Geofrey', 'Girard', 35, 'Lithium', '8-29-2009'),
(8, 5, 'Bill', 'McDougal', 35, 'Indocin', '9-30-2008'),
(9, 5, 'Bill', 'Jackson', 35, 'Celebrex', '10-31-2007')
SET IDENTITY_INSERT Pharmacist OFF;
            
GO


SET IDENTITY_INSERT Warehouse ON;
INSERT INTO Warehouse (Id, Name, Address, City, State, Zip)
VALUES
(1, 'Men''s Warehouse', '321 Main St.', 'Dallas', 'TX', 75201),
(2, 'Abandoned Building #7', '321 Main St.', 'Plano', 'TX', 75074),
(3, 'Smiles 4 Miles', '321 Main St.', 'Irving', 'TX', 75014)
SET IDENTITY_INSERT Warehouse OFF;
            
GO


INSERT INTO Delivery(PharmacyId, DrugName, UnitCount, UnitPrice, TotalPrice, WarehouseId, DeliveryDate)
VALUES
(1, 'Ibuprofen', 300, 0.08, 24, 1,  '11-20-2023'),
(2, 'Ibuprofen', 190, 0.08, 15.20, 2,  '11-21-2023'),
(3, 'Tylenol', 260, 0.10, 26, 3, '11-21-2023'),
(4, 'Tylenol', 240, 0.10, 24, 1, '11-24-2023'),
(5, 'Indocin', 200, 1.50, 300, 2, '11-28-2023'),
(1, 'Pepcid', 220, 1.50, 330, 3, '11-28-2023'),
(2, 'Amitriptyline', 130, 2.50, 325, 1, '11-29-2023'),
(3, 'Asprin', 270, 0.10, 27, 2, '11-30-2023'),
(4, 'Lithium', 220, 1.50, 330, 3, '12-4-2023'),
(5, 'Indocin', 190, 1.50, 285, 1, '12-5-2023'),
(1, 'Asprin', 240, 0.10, 24, 2, '12-8-2023'),
(2, 'Asprin', 100, 0.10, 10, 3, '12-9-2023'),
(3, 'Tylenol', 160, 0.10, 16, 1, '12-12-2023'),
(4, 'Pepcid', 220, 1.50, 330, 2, '12-12-2023'),
(5, 'Celebrex', 150, 1.50, 225, 3, '12-12-2023'),
(1, 'Pepcid', 200, 1.50, 300, 1, '12-14-2023'),
(2, 'Asprin', 220, 0.10, 22, 2, '12-18-2023'),
(3, 'Sudafed', 110, 0.15, 16.50, 3, '12-19-2023'),
(4, 'Asprin', 150, 0.10, 15, 1, '12-20-2023'),
(5, 'Asprin', 220, 0.10, 22, 2, '12-21-2023'),
(1, 'Ibuprofen', 130, 0.08, 10.4, 3, '12-22-2023'),
(2, 'Amitriptyline', 280, 2.50, 700, 1, '12-23-2023'),
(3, 'Sudafed', 100, 0.15, 15, 2, '12-24-2023'),
(4, 'Lithium', 170, 1.50, 255, 3, '12-26-2023')
            
GO

CREATE INDEX [IX_Delivery_PharmacyId] ON [Delivery] ([PharmacyId]);
GO

CREATE INDEX [IX_Delivery_WarehouseId] ON [Delivery] ([WarehouseId]);
GO

CREATE INDEX [IX_Pharmacist_PharmacyId] ON [Pharmacist] ([PharmacyId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231227204040_AddReportingTables', N'7.0.14');
GO

COMMIT;
GO

