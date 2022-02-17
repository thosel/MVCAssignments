using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCAssignments.Migrations
{
    public partial class SeedPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "CityId", "Name", "Phone" },
                values: new object[,]
                {
                    { 1000, 1000, "Sven Svensk", "1111111111" },
                    { 1001, 1001, "Pecka Finsk", "2222222222" },
                    { 1002, 1002, "Sigurður Isländsk", "3333333333" },
                    { 1003, 1003, "Preben Dansk", "4444444444" },
                    { 1004, 1004, "Ola Norman", "5555555555" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1000);

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
        }
    }
}
