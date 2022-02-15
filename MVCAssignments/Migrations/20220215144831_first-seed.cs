using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCAssignments.Migrations
{
    public partial class firstseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "City", "Name", "Phone" },
                values: new object[] { 1000, "Göteborg", "William Shakespeare", "031654321" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1000);
        }
    }
}
