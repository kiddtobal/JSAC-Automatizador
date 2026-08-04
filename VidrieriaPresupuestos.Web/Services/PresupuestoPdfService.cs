using System.Globalization;
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

                    page.Header().Element(c => ComponerEncabezado(c, presupuesto));
                    page.Content().Element(c => ComponerContenido(c, presupuesto, logoPath));
                    page.Footer().Element(ComponerFooter);
                });
            });

            return documento.GeneratePdf();
        }

        private void ComponerEncabezado(IContainer container, Presupuesto presupuesto)
        {
            container.PaddingBottom(10).Row(row =>
            {
                row.RelativeItem(3).Column(column =>
                {
                    column.Spacing(2);
                    column.Item().Text("Rimalto Ltda.").Bold();
                    column.Item().Text("RUT: 76.175.151-4");
                    column.Item().Text("Álvarez de Toledo 606, San Joaquín");
                    column.Item().Text("Fono: +56 2 2639 5459");
                    column.Item().Text("E-mail: rimalto@rimalto.cl");
                    column.Item().Text("Web: www.rimalto.cl");
                });

                row.RelativeItem(2).Column(column =>
                {
                    column.Item().Border(1).Padding(8).AlignCenter().Text($"Presupuesto N° {presupuesto.Id:0000}").Bold().FontSize(13);
                    column.Item().PaddingTop(4).AlignCenter().Text(TextoFecha(presupuesto.Fecha));
                });
            });
        }

        private static string TextoFecha(DateTime fecha)
        {
            var culturaCl = CultureInfo.GetCultureInfo("es-CL");
            return $"Santiago, {fecha.ToString("d 'de' MMMM 'del' yyyy", culturaCl)}";
        }

        private void ComponerContenido(IContainer container, Presupuesto presupuesto, string logoPath)
        {
            container.PaddingTop(10).Column(column =>
            {
                column.Spacing(10);

                column.Item().Column(destinatario =>
                {
                    var prefijo = presupuesto.Cliente.Genero == Genero.Masculino ? "Sr." : "Sra.";
                    destinatario.Item().Text($"{prefijo} {presupuesto.Cliente.Nombre}");

                    if (!string.IsNullOrWhiteSpace(presupuesto.Cliente.Cargo))
                    {
                        destinatario.Item().Text($"{presupuesto.Cliente.Cargo}.");
                    }

                    if (!string.IsNullOrWhiteSpace(presupuesto.Cliente.Empresa))
                    {
                        destinatario.Item().Text(presupuesto.Cliente.Empresa);
                    }
                });

                if (!string.IsNullOrWhiteSpace(presupuesto.Referencia))
                {
                    column.Item().Border(1).Padding(6).Text(text =>
                    {
                        text.Span("Referencia: ");
                        text.Span(presupuesto.Referencia).Bold();
                    });
                }

                column.Item().Border(1).Padding(6).Text(text =>
                {
                    text.Span("Local: ");
                    text.Span(presupuesto.Local).Bold();
                    text.Span("   Dirección: ");
                    text.Span(presupuesto.DireccionTrabajo).Bold();
                    text.Span("   Comuna: ");
                    text.Span(presupuesto.Comuna).Bold();
                });

                if (!string.IsNullOrWhiteSpace(presupuesto.DescripcionTrabajo))
                {
                    column.Item().Column(descripcion =>
                    {
                        descripcion.Item().Text("Descripción del Trabajo").Bold().FontSize(12);
                        descripcion.Item().PaddingTop(4).Text(presupuesto.DescripcionTrabajo);
                    });
                }

                column.Item().Column(tablaYSubtotal =>
                {
                    tablaYSubtotal.Spacing(0);
                    tablaYSubtotal.Item().Element(c => ComponerTablaItems(c, presupuesto));
                    tablaYSubtotal.Item().AlignRight().Border(1).Padding(6).Text($"Sub Total: {presupuesto.Subtotal:C}").Bold().FontSize(12);
                });

                if (presupuesto.CargosAdicionales.Count > 0)
                {
                    column.Item().Element(c => ComponerTablaCargos(c, presupuesto.CargosAdicionales));
                }

                column.Item().AlignRight().Border(1).Padding(8).Text($"Valor Neto: {presupuesto.ValorNeto:C}").Bold().FontSize(12);

                column.Item().PaddingTop(20).Column(cierre =>
                {
                    cierre.Spacing(2);
                    cierre.Item().Text("Saluda Atentamente,").Bold();
                    cierre.Item().Text(presupuesto.Cotizador.Nombre).Italic();
                    cierre.Item().Text($"Celular {presupuesto.Cotizador.Celular}");
                    cierre.Item().Text($"E-mail: {presupuesto.Cotizador.Email}");

                    if (File.Exists(logoPath))
                    {
                        cierre.Item().AlignCenter().Height(40).Image(logoPath);
                    }

                    cierre.Item().AlignCenter().Text("Servicio Integral en Obras Menores. Nuestros presupuestos están basados en un horario de trabajo de Lunes a Viernes de 9:00 a 17:00 hrs.").FontSize(8);
                });
            });
        }

        private void ComponerTablaItems(IContainer container, Presupuesto presupuesto)
        {
            var materiales = presupuesto.Items.Where(i => i.Seccion == SeccionItem.Materiales).ToList();
            var manoDeObra = presupuesto.Items.Where(i => i.Seccion == SeccionItem.ManoDeObra).ToList();

            if (materiales.Count == 0 && manoDeObra.Count == 0)
            {
                return;
            }

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
                    header.Cell().Border(1).Padding(4).Text("Descripción materiales").Bold();
                    header.Cell().Border(1).Padding(4).Text("Cantidad").Bold();
                    header.Cell().Border(1).Padding(4).Text("Unidad").Bold();
                    header.Cell().Border(1).Padding(4).Text("Valor Unit.").Bold();
                    header.Cell().Border(1).Padding(4).Text("Total").Bold();
                });

                foreach (var item in materiales)
                {
                    table.Cell().Border(1).Padding(4).Text(item.Descripcion);
                    table.Cell().Border(1).Padding(4).Text(item.Cantidad.ToString());
                    table.Cell().Border(1).Padding(4).Text(item.Unidad);
                    table.Cell().Border(1).Padding(4).Text(item.ValorUnitario.ToString("C"));
                    table.Cell().Border(1).Padding(4).Text(item.Total.ToString("C"));
                }

                if (manoDeObra.Count > 0)
                {
                    table.Cell().ColumnSpan(5).Border(1).Padding(4).Text("Mano de Obra").Bold();

                    foreach (var item in manoDeObra)
                    {
                        table.Cell().Border(1).Padding(4).Text(item.Descripcion);
                        table.Cell().Border(1).Padding(4).Text(item.Cantidad.ToString());
                        table.Cell().Border(1).Padding(4).Text(item.Unidad);
                        table.Cell().Border(1).Padding(4).Text(item.ValorUnitario.ToString("C"));
                        table.Cell().Border(1).Padding(4).Text(item.Total.ToString("C"));
                    }
                }
            });
        }

        private void ComponerTablaCargos(IContainer container, ICollection<CargoAdicional> cargos)
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
                    header.Cell().Border(1).Padding(4).Text("Cargos Adicionales").Bold();
                    header.Cell().Border(1).Padding(4).Text("Cantidad").Bold();
                    header.Cell().Border(1).Padding(4).Text("Unidad").Bold();
                    header.Cell().Border(1).Padding(4).Text("Valor Unit.").Bold();
                    header.Cell().Border(1).Padding(4).Text("Total").Bold();
                });

                foreach (var cargo in cargos)
                {
                    table.Cell().Border(1).Padding(4).Text(cargo.Nombre);

                    if (cargo.TipoValor == TipoValorCargo.Porcentaje)
                    {
                        table.Cell().Border(1).Padding(4).Text($"{cargo.Valor}%");
                        table.Cell().Border(1).Padding(4).Text("");
                        table.Cell().Border(1).Padding(4).Text("");
                    }
                    else
                    {
                        table.Cell().Border(1).Padding(4).Text(cargo.Cantidad.ToString());
                        table.Cell().Border(1).Padding(4).Text(cargo.Unidad ?? "");
                        table.Cell().Border(1).Padding(4).Text(cargo.Valor.ToString("C"));
                    }

                    table.Cell().Border(1).Padding(4).Text(cargo.MontoCalculado.ToString("C"));
                }
            });
        }

        private void ComponerFooter(IContainer container)
        {
            container.AlignRight().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
        }
    }
}
