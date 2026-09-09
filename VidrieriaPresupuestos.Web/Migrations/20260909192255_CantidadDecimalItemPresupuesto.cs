using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class CantidadDecimalItemPresupuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Cantidad",
                table: "ItemsPresupuesto",
                type: "TEXT",
                precision: 10,
                scale: 4,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cantidad",
                table: "ItemsPresupuesto",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldPrecision: 10,
                oldScale: 4);
        }
    }
}
