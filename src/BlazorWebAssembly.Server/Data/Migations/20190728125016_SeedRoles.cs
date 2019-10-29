using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorWebAssembly.Server.Data.Migations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "37164cce-ffcb-484e-8501-9ea116c925f7", "6434d7e4-c81c-4d80-8f15-6a6307df3b7f", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecdaa209-2c35-4e27-acb7-f8d173ce1dda", "2d4420f4-ced7-4896-9a1f-a185a0919618", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37164cce-ffcb-484e-8501-9ea116c925f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecdaa209-2c35-4e27-acb7-f8d173ce1dda");
        }
    }
}
