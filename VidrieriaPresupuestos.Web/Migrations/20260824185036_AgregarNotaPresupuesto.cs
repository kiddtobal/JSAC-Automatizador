using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNotaPresupuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nota",
                table: "Presupuestos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota",
                table: "Presupuestos");
        }
    }
}
