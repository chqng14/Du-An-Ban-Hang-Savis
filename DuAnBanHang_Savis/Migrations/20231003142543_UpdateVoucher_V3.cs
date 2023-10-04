using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data.Migrations
{
    public partial class UpdateVoucher_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DieuKien",
                table: "Vouchers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DieuKien",
                table: "Vouchers",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
