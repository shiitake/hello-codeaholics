using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloCodeaholics.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReportingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pharmacist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PrimaryDrugSold = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfHire = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacist_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Zip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    DrugName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitCount = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Delivery_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            // add pharmacists
            migrationBuilder.Sql(
            @"
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
            ");

            // add the warehouses
            migrationBuilder.Sql(
            @"
            SET IDENTITY_INSERT Warehouse ON;
            INSERT INTO Warehouse (Id, Name, Address, City, State, Zip)
            VALUES
            (1, 'Men''s Warehouse', '321 Main St.', 'Dallas', 'TX', 75201),
            (2, 'Abandoned Building #7', '321 Main St.', 'Plano', 'TX', 75074),
            (3, 'Smiles 4 Miles', '321 Main St.', 'Irving', 'TX', 75014)
            SET IDENTITY_INSERT Warehouse OFF;
            ");

            // add the deliveries
            migrationBuilder.Sql(
            @"
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
            ");
            migrationBuilder.CreateIndex(
                name: "IX_Delivery_PharmacyId",
                table: "Delivery",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_WarehouseId",
                table: "Delivery",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacist_PharmacyId",
                table: "Pharmacist",
                column: "PharmacyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.DropTable(
                name: "Pharmacist");

            migrationBuilder.DropTable(
                name: "Warehouse");
        }
    }
}
