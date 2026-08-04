using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class CamposCompletosParaPdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreadoPor",
                table: "Presupuestos",
                newName: "Comuna");

            migrationBuilder.AddColumn<int>(
                name: "CotizadorId",
                table: "Presupuestos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DireccionTrabajo",
                table: "Presupuestos",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Presupuestos",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Referencia",
                table: "Presupuestos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "Clientes",
                type: "TEXT",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Empresa",
                table: "Clientes",
                type: "TEXT",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Genero",
                table: "Clientes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "CargosAdicionales",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Unidad",
                table: "CargosAdicionales",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cotizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cargo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Celular = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizadores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Presupuestos_CotizadorId",
                table: "Presupuestos",
                column: "CotizadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presupuestos_Cotizadores_CotizadorId",
                table: "Presupuestos",
                column: "CotizadorId",
                principalTable: "Cotizadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presupuestos_Cotizadores_CotizadorId",
                table: "Presupuestos");

            migrationBuilder.DropTable(
                name: "Cotizadores");

            migrationBuilder.DropIndex(
                name: "IX_Presupuestos_CotizadorId",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "CotizadorId",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "DireccionTrabajo",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "Local",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "Referencia",
                table: "Presupuestos");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Empresa",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "CargosAdicionales");

            migrationBuilder.DropColumn(
                name: "Unidad",
                table: "CargosAdicionales");

            migrationBuilder.RenameColumn(
                name: "Comuna",
                table: "Presupuestos",
                newName: "CreadoPor");
        }
    }
}
