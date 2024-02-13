using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseSynchServiceUsingWorkerService.Migrations
{
    /// <inheritdoc />
    public partial class AddedRowInTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Table1",
                columns: new[] { "Id", "Contact", "Name" },
                values: new object[] { 4, "9876765212", "AC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Table1",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
