using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManager.Infrastructure.Persistence.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20E5D261-073E-4395-9E83-BEEC2DCD2EC4", "F2CE9090-4855-4E8E-BCCB-0B75C5B3BFAB", "Klient", "KLIENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6E9BEA3A-DD4A-4D24-BA70-ABCE4D1CB47F", "3EB92419-3A20-48CF-BF34-8457B8B9A96D", "Pracownik", "PRACOWNIK" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "80B6EE4B-8640-4D67-8636-C7BFA8F2451D", "B89055F6-4241-4A90-8163-7B845C099FAA", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20E5D261-073E-4395-9E83-BEEC2DCD2EC4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6E9BEA3A-DD4A-4D24-BA70-ABCE4D1CB47F");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80B6EE4B-8640-4D67-8636-C7BFA8F2451D");
        }
    }
}
