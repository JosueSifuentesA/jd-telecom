using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class SevenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "serviciosId_servicios",
                table: "usuario",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_serviciosId_servicios",
                table: "usuario",
                column: "serviciosId_servicios");

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_servicios_serviciosId_servicios",
                table: "usuario",
                column: "serviciosId_servicios",
                principalTable: "servicios",
                principalColumn: "id_servicios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuario_servicios_serviciosId_servicios",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "IX_usuario_serviciosId_servicios",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "serviciosId_servicios",
                table: "usuario");
        }
    }
}
