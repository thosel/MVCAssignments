using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCAssignments.Migrations
{
    public partial class secondseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1000,
                column: "Name",
                value: "Kålle Glagogubbe");

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "City", "Name", "Phone" },
                values: new object[,]
                {
                    { 1001, "Stockholm", "Sune Söderkis", "08654321" },
                    { 1002, "Luleå", "Enok Evertsson", "0704654321" },
                    { 1003, "Avesta", "Alva Alm", "0703216541" },
                    { 1004, "Göteborg", "Ted Rajtantajtansson", "031321654" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1000,
                column: "Name",
                value: "William Shakespeare");
        }
    }
}
