using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class FiveMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "velocidad",
                table: "planes",
                newName: "velocidad_plan");

            migrationBuilder.RenameColumn(
                name: "precio",
                table: "planes",
                newName: "precio_plan");

            migrationBuilder.RenameColumn(
                name: "descripcion",
                table: "planes",
                newName: "nombre_plan");

            migrationBuilder.AddColumn<string>(
                name: "descripcion_plan",
                table: "planes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descripcion_plan",
                table: "planes");

            migrationBuilder.RenameColumn(
                name: "velocidad_plan",
                table: "planes",
                newName: "velocidad");

            migrationBuilder.RenameColumn(
                name: "precio_plan",
                table: "planes",
                newName: "precio");

            migrationBuilder.RenameColumn(
                name: "nombre_plan",
                table: "planes",
                newName: "descripcion");
        }
    }
}
