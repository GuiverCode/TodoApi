using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class Datos_Semilla_Prioridad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Prioridades",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 1L, "Baja" });

            migrationBuilder.InsertData(
                table: "Prioridades",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 2L, "Media" });

            migrationBuilder.InsertData(
                table: "Prioridades",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 3L, "Alta" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prioridades",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Prioridades",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Prioridades",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
