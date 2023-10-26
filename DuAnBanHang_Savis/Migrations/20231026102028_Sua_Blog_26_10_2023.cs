using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data.Migrations
{
    public partial class Sua_Blog_26_10_2023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "MoTa",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TenBlog",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Vouchers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "Blogs",
                type: "Nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ma",
                table: "Blogs",
                type: "Varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MoTaNgan",
                table: "Blogs",
                type: "Nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "Blogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenAnh",
                table: "Blogs",
                type: "Nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TieuDe",
                table: "Blogs",
                type: "Nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Ma", "Ten", "TrangThai" },
                values: new object[] { new Guid("2a923806-d3c0-4937-9ec1-916e0eceda75"), "USER", "user", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DiaChi", "Email", "GioiTinh", "IdRole", "Ma", "MatKhau", "NgaySinh", "Sdt", "TaiKhoan", "Ten", "TrangThai" },
                values: new object[] { new Guid("c6649263-8c52-47c1-809e-e483c39f437a"), " ", "admin@gmail.com", 0, new Guid("7430128c-674e-4c3d-9f6a-9c4025ae22fe"), "ADMIN", "admin", new DateTime(2023, 10, 26, 17, 20, 27, 921, DateTimeKind.Local).AddTicks(5983), "0987654321", "admin", "Admin", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2a923806-d3c0-4937-9ec1-916e0eceda75"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c6649263-8c52-47c1-809e-e483c39f437a"));

            migrationBuilder.DropColumn(
                name: "MoTaNgan",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TenAnh",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "TieuDe",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Ten",
                table: "Vouchers",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "NoiDung",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ma",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(50)");

            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenBlog",
                table: "Blogs",
                type: "nvarchar(max)",
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
    }
}
