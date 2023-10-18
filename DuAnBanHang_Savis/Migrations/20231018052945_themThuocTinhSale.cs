using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data.Migrations
{
    public partial class themThuocTinhSale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("85d5a399-ceae-4323-8754-674d50690bbd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8e63ebc5-838e-44e9-bf74-f15471a842d8"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayBatDau",
                table: "Sale",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<string>(
                name: "DuongDanAnh",
                table: "Sale",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Ma", "Ten", "TrangThai" },
                values: new object[] { new Guid("cb9ed66e-c971-4f2a-851d-088026eee58b"), "USER", "user", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DiaChi", "Email", "GioiTinh", "IdRole", "Ma", "MatKhau", "NgaySinh", "Sdt", "TaiKhoan", "Ten", "TrangThai" },
                values: new object[] { new Guid("d5ace288-10ac-4f71-a430-60450d7a1bc4"), " ", "admin@gmail.com", 0, new Guid("7430128c-674e-4c3d-9f6a-9c4025ae22fe"), "ADMIN", "admin", new DateTime(2023, 10, 18, 12, 29, 45, 594, DateTimeKind.Local).AddTicks(564), "0987654321", "admin", "Admin", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cb9ed66e-c971-4f2a-851d-088026eee58b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d5ace288-10ac-4f71-a430-60450d7a1bc4"));

            migrationBuilder.DropColumn(
                name: "DuongDanAnh",
                table: "Sale");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayBatDau",
                table: "Sale",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Ma", "Ten", "TrangThai" },
                values: new object[] { new Guid("85d5a399-ceae-4323-8754-674d50690bbd"), "USER", "user", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DiaChi", "Email", "GioiTinh", "IdRole", "Ma", "MatKhau", "NgaySinh", "Sdt", "TaiKhoan", "Ten", "TrangThai" },
                values: new object[] { new Guid("8e63ebc5-838e-44e9-bf74-f15471a842d8"), " ", "admin@gmail.com", 0, new Guid("7430128c-674e-4c3d-9f6a-9c4025ae22fe"), "ADMIN", "admin", new DateTime(2023, 10, 12, 15, 29, 0, 555, DateTimeKind.Local).AddTicks(483), "0987654321", "admin", "Admin", 0 });
        }
    }
}
