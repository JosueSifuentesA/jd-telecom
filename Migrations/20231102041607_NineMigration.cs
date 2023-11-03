using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class NineMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promocion_usuario_usuarioid_usuario",
                table: "promocion");

            migrationBuilder.DropIndex(
                name: "IX_promocion_usuarioid_usuario",
                table: "promocion");

            migrationBuilder.DropColumn(
                name: "usuarioid_usuario",
                table: "promocion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "usuarioid_usuario",
                table: "promocion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_promocion_usuarioid_usuario",
                table: "promocion",
                column: "usuarioid_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_promocion_usuario_usuarioid_usuario",
                table: "promocion",
                column: "usuarioid_usuario",
                principalTable: "usuario",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
