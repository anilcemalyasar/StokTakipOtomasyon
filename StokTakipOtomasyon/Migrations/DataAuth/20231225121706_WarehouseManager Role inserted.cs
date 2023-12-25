using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokTakipOtomasyon.Migrations.DataAuth
{
    /// <inheritdoc />
    public partial class WarehouseManagerRoleinserted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b21972e1-742f-4fa7-be46-1189d9cab7ca", "b21972e1-742f-4fa7-be46-1189d9cab7ca", "WarehouseManager", "WAREHOUSEMANAGER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b21972e1-742f-4fa7-be46-1189d9cab7ca");
        }
    }
}
