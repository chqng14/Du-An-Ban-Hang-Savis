using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App_Data.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Carts_CartsIdUser",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CartsIdUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CartsIdUser",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_IdUser",
                table: "Carts",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_IdUser",
                table: "Carts");

            migrationBuilder.AddColumn<Guid>(
                name: "CartsIdUser",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CartsIdUser",
                table: "Users",
                column: "CartsIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Carts_CartsIdUser",
                table: "Users",
                column: "CartsIdUser",
                principalTable: "Carts",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
