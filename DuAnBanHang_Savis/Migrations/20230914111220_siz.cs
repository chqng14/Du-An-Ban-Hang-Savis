using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data.Migrations
{
    public partial class siz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "Size",
                newName: "Size1");

            migrationBuilder.AddColumn<decimal>(
                name: "Cm",
                table: "Size",
                type: "decimal",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "Size",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cm",
                table: "Size");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "Size");

            migrationBuilder.RenameColumn(
                name: "Size1",
                table: "Size",
                newName: "Ten");
        }
    }
}
