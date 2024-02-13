using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatabaseSynchServiceUsingWorkerService.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTheData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Table1",
                columns: new[] { "Id", "Contact", "Name" },
                values: new object[,]
                {
                    { 1, "9876546987", "SmartPhone" },
                    { 2, "9879046987", "Refrigerators" },
                    { 3, "9812346987", "TV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Table1",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Table1",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Table1",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
