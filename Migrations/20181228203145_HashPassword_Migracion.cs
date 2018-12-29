using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class HashPassword_Migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Usuarios",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<byte[]>(
                name: "HashPassword",
                table: "Usuarios",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuarios",
                nullable: false,
                defaultValue: "");
        }
    }
}
