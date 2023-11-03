using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDTelecomunicaciones.Migrations
{
    /// <inheritdoc />
    public partial class EightMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "imgSubidaByte",
                table: "promocion",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgSubidaByte",
                table: "promocion");
        }
    }
}
