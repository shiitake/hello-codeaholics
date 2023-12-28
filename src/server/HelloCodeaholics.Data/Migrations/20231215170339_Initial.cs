using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloCodeaholics.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    FilledPrescriptionsCount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.Id);
                });

            // add some initial data
            migrationBuilder.Sql(
            @"
            INSERT INTO Pharmacy (Name, Address, City, State, Zip, FilledPrescriptionsCount, CreatedDate)
            VALUES
            ('Bill''s Pharmacy', '123 Main St.', 'Dallas', 'TX', 75201, 0, GETDATE()),
            ('Barbs''s Pharmacy', '123 Main St.', 'Plano', 'TX', 75074, 0, GETDATE()),
            ('House O Pills', '123 Main St.', 'Irving', 'TX', 75014, 0, GETDATE()),
            ('RX R US', '123 Main St.', 'Ft Worth', 'TX', 75050, 0, GETDATE()),
            ('Los Druggos', '123 Main St.', 'Arlington', 'TX', 76001, 0, GETDATE())
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pharmacy");
        }
    }
}
