using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Boxes.Shared.Entites;

public class InvoiceService
{
    public byte[] GenerateInvoice(List<Producto> products, decimal total)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Header().Text("Factura de Compra").FontSize(20).Bold();

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(200);
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Producto").Bold();
                        header.Cell().Text("Precio").Bold();
                    });

                    foreach (var product in products)
                    {
                        table.Cell().Text(product.Name);
                        table.Cell().Text($"{product.Price:C2}");
                    }

                    table.Cell().Text("TOTAL").Bold();
                    table.Cell().Text($"{total:C2}").Bold();
                });

                page.Footer().AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
            });
        });

        return document.GeneratePdf();
    }
}