using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VidrieriaPresupuestos.Web.Migrations
{
    /// <inheritdoc />
    public partial class CatalogoDeProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CategoriaPadreId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_CategoriaPadreId",
                        column: x => x.CategoriaPadreId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "ProductosCatalogo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Unidad = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Origen = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecioBase = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    PorcentajeComision = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCatalogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductosCatalogo_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductoAtributoValores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductoCatalogoId = table.Column<int>(type: "INTEGER", nullable: false),
                    AtributoDefinicionId = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "IX_Categorias_CategoriaPadreId",
                table: "Categorias",
                column: "CategoriaPadreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoAtributoValores_AtributoDefinicionId",
                table: "ProductoAtributoValores",
                column: "AtributoDefinicionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoAtributoValores_ProductoCatalogoId",
                table: "ProductoAtributoValores",
                column: "ProductoCatalogoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCatalogo_CategoriaId",
                table: "ProductosCatalogo",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoAtributoValores");

            migrationBuilder.DropTable(
                name: "AtributosDefinicion");

            migrationBuilder.DropTable(
                name: "ProductosCatalogo");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
