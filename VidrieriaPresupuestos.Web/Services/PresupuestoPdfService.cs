using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using VidrieriaPresupuestos.Domain.Entidades;

namespace VidrieriaPresupuestos.Web.Services
{
    public class PresupuestoPdfService
    {
        private readonly IWebHostEnvironment _env;

        public PresupuestoPdfService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public byte[] GenerarPdf(Presupuesto presupuesto)
        {
            var logoPath = Path.Combine(_env.WebRootPath, "images", "logo.png");

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(c => ComponerEncabezado(c, logoPath));
                    page.Content().Element(c => ComponerContenido(c, presupuesto));
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });

            return documento.GeneratePdf();
        }

        private void ComponerEncabezado(IContainer container, string logoPath)
        {
            container.Row(row =>
            {
                if (File.Exists(logoPath))
                {
                    row.ConstantItem(120).Image(logoPath);
                }

                row.RelativeItem().AlignRight().Column(column =>
                {
                    column.Item().Text("Rimalto LTDA").Bold();
                    column.Item().Text("+56 2 2639 5459");
                    column.Item().Text("rimalto@rimalto.cl");
                    column.Item().Text("Álvarez de Toledo 606, San Joaquín, RM");
                });
            });
        }

        private void ComponerContenido(IContainer container, Presupuesto presupuesto)
        {
            container.PaddingTop(20).Column(column =>
            {
                column.Spacing(10);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Text($"Presupuesto N° {presupuesto.Id:0000}").Bold().FontSize(14);
                    row.RelativeItem().AlignRight().Text(presupuesto.Fecha.ToString("dd/MM/yyyy"));
                });

                column.Item().Column(clienteColumn =>
                {
                    clienteColumn.Item().Text("Cliente").Bold();
                    clienteColumn.Item().Text(presupuesto.Cliente.Nombre);

                    if (!string.IsNullOrWhiteSpace(presupuesto.Cliente.Telefono))
                    {
                        clienteColumn.Item().Text($"Tel: {presupuesto.Cliente.Telefono}");
                    }

                    if (!string.IsNullOrWhiteSpace(presupuesto.Cliente.Email))
                    {
                        clienteColumn.Item().Text($"Email: {presupuesto.Cliente.Email}");
                    }
                });

                if (!string.IsNullOrWhiteSpace(presupuesto.DescripcionTrabajo))
                {
                    column.Item().Column(descripcionColumn =>
                    {
                        descripcionColumn.Item().Text("Descripción del Trabajo").Bold();
                        descripcionColumn.Item().Text(presupuesto.DescripcionTrabajo);
                    });
                }

                var materiales = presupuesto.Items.Where(i => i.Seccion == SeccionItem.Materiales).ToList();
                if (materiales.Count > 0)
                {
                    column.Item().Text("Materiales").Bold().FontSize(12);
                    column.Item().Element(c => ComponerTablaItems(c, materiales));
                }

                var manoDeObra = presupuesto.Items.Where(i => i.Seccion == SeccionItem.ManoDeObra).ToList();
                if (manoDeObra.Count > 0)
                {
                    column.Item().Text("Mano de Obra").Bold().FontSize(12);
                    column.Item().Element(c => ComponerTablaItems(c, manoDeObra));
                }

                column.Item().AlignRight().Text($"Subtotal: {presupuesto.Subtotal:C}").Bold().FontSize(12);

                if (presupuesto.CargosAdicionales.Count > 0)
                {
                    column.Item().Text("Cargos Adicionales").Bold().FontSize(12);
                    column.Item().Element(c => ComponerTablaCargos(c, presupuesto.CargosAdicionales));
                }

                column.Item().AlignRight().Text($"Valor Neto: {presupuesto.ValorNeto:C}").Bold().FontSize(16);

                column.Item().PaddingTop(30).Text("Saluda Atentamente,");
                // TODO: fix temporal - se reemplaza en la Fase B con el flujo real de selección de Cotizador
                column.Item().PaddingTop(30).Text(presupuesto.Cotizador?.Nombre ?? "");
            });
        }

        private void ComponerTablaItems(IContainer container, List<ItemPresupuesto> items)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Descripción").Bold();
                    header.Cell().Text("Unidad").Bold();
                    header.Cell().Text("Cantidad").Bold();
                    header.Cell().Text("Valor Unit.").Bold();
                    header.Cell().Text("Total").Bold();
                });

                foreach (var item in items)
                {
                    table.Cell().Text(item.Descripcion);
                    table.Cell().Text(item.Unidad);
                    table.Cell().Text(item.Cantidad.ToString());
                    table.Cell().Text(item.ValorUnitario.ToString("C"));
                    table.Cell().Text(item.Total.ToString("C"));
                }
            });
        }

        private void ComponerTablaCargos(IContainer container, ICollection<CargoAdicional> cargos)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                });

                table.Header(header =>
                {
                    header.Cell().Text("Nombre").Bold();
                    header.Cell().Text("Tipo").Bold();
                    header.Cell().Text("Valor").Bold();
                    header.Cell().Text("Monto").Bold();
                });

                foreach (var cargo in cargos)
                {
                    table.Cell().Text(cargo.Nombre);
                    table.Cell().Text(cargo.TipoValor.ToString());
                    table.Cell().Text(cargo.TipoValor == TipoValorCargo.Porcentaje ? $"{cargo.Valor}%" : cargo.Valor.ToString("C"));
                    table.Cell().Text(cargo.MontoCalculado.ToString("C"));
                }
            });
        }
    }
}
