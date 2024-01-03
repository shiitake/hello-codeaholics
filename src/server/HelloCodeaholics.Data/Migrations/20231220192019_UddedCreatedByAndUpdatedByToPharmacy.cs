using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelloCodeaholics.Data.Migrations
{
    /// <inheritdoc />
    public partial class UddedCreatedByAndUpdatedByToPharmacy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Pharmacy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "HelloCode");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Pharmacy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Pharmacy");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Pharmacy");
        }
    }
}
