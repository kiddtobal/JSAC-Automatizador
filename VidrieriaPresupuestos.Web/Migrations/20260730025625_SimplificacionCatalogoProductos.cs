using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class SimplificacionCatalogoProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoAtributoValores");

            migrationBuilder.DropTable(
                name: "AtributosDefinicion");

            migrationBuilder.AddColumn<string>(
                name: "UrlReferencia",
                table: "ProductosCatalogo",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlReferencia",
                table: "ProductosCatalogo");

            migrationBuilder.CreateTable(
                name: "AtributosDefinicion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TipoDato = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtributosDefinicion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtributosDefinicion_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoAtributoValores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AtributoDefinicionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoCatalogoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoAtributoValores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoAtributoValores_AtributosDefinicion_AtributoDefinicionId",
                        column: x => x.AtributoDefinicionId,
                        principalTable: "AtributosDefinicion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductoAtributoValores_ProductosCatalogo_ProductoCatalogoId",
                        column: x => x.ProductoCatalogoId,
                        principalTable: "ProductosCatalogo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtributosDefinicion_CategoriaId",
                table: "AtributosDefinicion",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoAtributoValores_AtributoDefinicionId",
                table: "ProductoAtributoValores",
                column: "AtributoDefinicionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoAtributoValores_ProductoCatalogoId",
                table: "ProductoAtributoValores",
                column: "ProductoCatalogoId");
        }
    }
}
