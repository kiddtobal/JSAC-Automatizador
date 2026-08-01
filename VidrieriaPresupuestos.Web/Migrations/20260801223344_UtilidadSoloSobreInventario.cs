using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class UtilidadSoloSobreInventario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CalcularSoloSobreInventario",
                table: "CargosAdicionales",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalcularSoloSobreInventario",
                table: "CargosAdicionales");
        }
    }
}
