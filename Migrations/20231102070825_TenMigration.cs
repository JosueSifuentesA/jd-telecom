using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class TenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "promocionesid_promocion",
                table: "usuario",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_promocionesid_promocion",
                table: "usuario",
                column: "promocionesid_promocion");

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_promocion_promocionesid_promocion",
                table: "usuario",
                column: "promocionesid_promocion",
                principalTable: "promocion",
                principalColumn: "id_promocion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuario_promocion_promocionesid_promocion",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "IX_usuario_promocionesid_promocion",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "promocionesid_promocion",
                table: "usuario");
        }
    }
}
