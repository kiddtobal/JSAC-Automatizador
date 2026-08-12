using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroCotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroCotizacion",
                table: "Presupuestos",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroCotizacion",
                table: "Presupuestos");
        }
    }
}
