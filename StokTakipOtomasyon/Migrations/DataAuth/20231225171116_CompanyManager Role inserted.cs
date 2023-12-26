using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokTakipOtomasyon.Migrations.DataAuth
{
    /// <inheritdoc />
    public partial class CompanyManagerRoleinserted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a91a40d3-af24-4e4d-89f3-8794a720e182", "a91a40d3-af24-4e4d-89f3-8794a720e182", "CompanyManager", "COMPANYMANAGER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a91a40d3-af24-4e4d-89f3-8794a720e182");
        }
    }
}
