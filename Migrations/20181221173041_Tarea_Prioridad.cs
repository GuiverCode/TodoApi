using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TodoApi.Migrations
{
    public partial class Tarea_Prioridad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaVencimiento",
                table: "Tareas",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IdPrioridad",
                table: "Tareas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prioridades",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prioridades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_IdPrioridad",
                table: "Tareas",
                column: "IdPrioridad");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Prioridades_IdPrioridad",
                table: "Tareas",
                column: "IdPrioridad",
                principalTable: "Prioridades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Prioridades_IdPrioridad",
                table: "Tareas");

            migrationBuilder.DropTable(
                name: "Prioridades");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_IdPrioridad",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "FechaVencimiento",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "IdPrioridad",
                table: "Tareas");
        }
    }
}
