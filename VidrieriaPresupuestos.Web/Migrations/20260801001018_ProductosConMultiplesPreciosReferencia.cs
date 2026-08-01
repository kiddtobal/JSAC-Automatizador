using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class ProductosConMultiplesPreciosReferencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioBase",
                table: "ProductosCatalogo");

            migrationBuilder.DropColumn(
                name: "UrlReferencia",
                table: "ProductosCatalogo");

            migrationBuilder.CreateTable(
                name: "PreciosReferencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductoCatalogoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Proveedor = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    UrlReferencia = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreciosReferencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreciosReferencia_ProductosCatalogo_ProductoCatalogoId",
                        column: x => x.ProductoCatalogoId,
                        principalTable: "ProductosCatalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreciosReferencia_ProductoCatalogoId",
                table: "PreciosReferencia",
                column: "ProductoCatalogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreciosReferencia");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioBase",
                table: "ProductosCatalogo",
                type: "TEXT",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UrlReferencia",
                table: "ProductosCatalogo",
                type: "TEXT",
                nullable: true);
        }
    }
}
