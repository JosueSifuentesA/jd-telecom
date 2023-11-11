using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class ThirtyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tecnicoDesignadoid_usuario",
                table: "ticket",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanReseñaid",
                table: "reseñas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ticket_tecnicoDesignadoid_usuario",
                table: "ticket",
                column: "tecnicoDesignadoid_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_reseñas_PlanReseñaid",
                table: "reseñas",
                column: "PlanReseñaid");

            migrationBuilder.AddForeignKey(
                name: "FK_reseñas_planes_PlanReseñaid",
                table: "reseñas",
                column: "PlanReseñaid",
                principalTable: "planes",
                principalColumn: "id_plan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticket_usuario_tecnicoDesignadoid_usuario",
                table: "ticket",
                column: "tecnicoDesignadoid_usuario",
                principalTable: "usuario",
                principalColumn: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reseñas_planes_PlanReseñaid",
                table: "reseñas");

            migrationBuilder.DropForeignKey(
                name: "FK_ticket_usuario_tecnicoDesignadoid_usuario",
                table: "ticket");

            migrationBuilder.DropIndex(
                name: "IX_ticket_tecnicoDesignadoid_usuario",
                table: "ticket");

            migrationBuilder.DropIndex(
                name: "IX_reseñas_PlanReseñaid",
                table: "reseñas");

            migrationBuilder.DropColumn(
                name: "tecnicoDesignadoid_usuario",
                table: "ticket");

            migrationBuilder.DropColumn(
                name: "PlanReseñaid",
                table: "reseñas");
        }
    }
}
