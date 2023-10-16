using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data.Migrations
{
    public partial class update_productDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaoHanh",
                table: "ProductDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "GiaThucTe",
                table: "ProductDetails",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaThucTe",
                table: "ProductDetails");

            migrationBuilder.AddColumn<string>(
                name: "BaoHanh",
                table: "ProductDetails",
                type: "nvarchar(250)",
                nullable: true);
        }
    }
}
