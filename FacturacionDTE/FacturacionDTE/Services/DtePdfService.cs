using FacturacionDTE.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace FacturacionDTE.Services
{
    public class DtePdfService
    {
        public byte[] GenerarPdf(FacturaFrontend factura)
        {
            var doc = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text("Factura Electrónica - DTE")
                        .SemiBold().FontSize(16).AlignCenter().Underline();

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Número de control: {factura.NumeroControl}");
                        col.Item().Text($"Número de factura: {factura.NumeroFactura}");
                        col.Item().Text($"Fecha emisión: {factura.FechaEmision:d}");
                        col.Item().Text($"Tipo DTE: {factura.TipoDte}");
                        col.Item().LineHorizontal(1);

                        col.Item().Text($"Cliente: {factura.ClienteNombre}");
                        col.Item().Text($"Documento: {factura.ClienteDocumento}");
                        col.Item().Text($"Departamento: {factura.ClienteDepartamento}");
                        col.Item().Text($"Municipio: {factura.ClienteMunicipio}");
                        col.Item().LineHorizontal(1);

                        // --- Detalle ---
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30);  // N°
                                columns.RelativeColumn(4);   // Descripción
                                columns.RelativeColumn(1);   // Cantidad
                                columns.RelativeColumn(2);   // Precio
                                columns.RelativeColumn(2);   // Subtotal
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("N°");
                                header.Cell().Text("Descripción");
                                header.Cell().Text("Cantidad");
                                header.Cell().Text("Precio U.");
                                header.Cell().Text("Subtotal");
                            });

                            foreach (var item in factura.Detalle)
                            {
                                table.Cell().Text(item.NroLinea.ToString());
                                table.Cell().Text(item.Descripcion);
                                table.Cell().Text(item.Cantidad.ToString());
                                table.Cell().Text($"${item.PrecioUnitario:F2}");
                                table.Cell().Text($"${item.Subtotal:F2}");
                            }
                        });

                        col.Item().LineHorizontal(1);
                        col.Item().Text($"Total Gravadas: ${factura.TotalGravadas:F2}");
                        col.Item().Text($"IVA (13%): ${factura.Iva:F2}");
                        col.Item().Text($"Total a Pagar: ${factura.TotalPagar:F2}").Bold();
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Documento Tributario Electrónico Simulado - No tiene validez fiscal")
                        .FontSize(9).Italic().FontColor(Colors.Grey.Darken1);
                });
            });

            using var stream = new MemoryStream();
            doc.GeneratePdf(stream);
            return stream.ToArray();
        }
    }
}

