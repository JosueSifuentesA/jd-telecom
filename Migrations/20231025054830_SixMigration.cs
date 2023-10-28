using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class SixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "servicios",
                columns: table => new
                {
                    id_servicios = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaActivacion_Servicio = table.Column<string>(type: "text", nullable: false),
                    PeriodoFacturacion_Servicio = table.Column<string>(type: "text", nullable: false),
                    Estado_Servicio = table.Column<char>(type: "character(1)", nullable: false),
                    Plan_Servicioid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicios", x => x.id_servicios);
                    table.ForeignKey(
                        name: "FK_servicios_planes_Plan_Servicioid",
                        column: x => x.Plan_Servicioid,
                        principalTable: "planes",
                        principalColumn: "id_plan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_servicios_Plan_Servicioid",
                table: "servicios",
                column: "Plan_Servicioid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "servicios");
        }
    }
}
