using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace FluentPOS.Modules.Invoicing.Core.Dtos.Prints
{
    public class LoadSheetDocument : IDocument
    {

        public LoadSheetMainDto Model { get; }

        public LoadSheetDocument(LoadSheetMainDto model)
        {
            Model = model;
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(5);

                    page.Content().Element(ComposeContent);
                });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Element(ComposeTable4);
            });
        }

        void ComposeTable4(IContainer container)
        {
            var headerStyle = TextStyle.Default.SemiBold();

            string folderName = Path.Combine("Files", "Images");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string fullPath = Path.Combine(pathToSave, "miniso.png");
            Image LogoImage = Image.FromFile(fullPath);

            container.Border(1)
                .Table(table =>
                {
                    IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                    {
                        return container
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Background(backgroundColor)
                            .PaddingVertical(5)
                            .PaddingHorizontal(10)
                            .AlignCenter()
                            .AlignMiddle();
                    }
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        // please be sure to call the 'header' handler!

                        header.Cell().Element(CellStyle).Text("Sr#");

                        header.Cell().Element(CellStyle).Text("Order No.");
                        header.Cell().Element(CellStyle).Text("Tracking No.");

                        header.Cell().Element(CellStyle).Text("City");
                        header.Cell().Element(CellStyle).Text("Payment");

                        header.Cell().Element(CellStyle).Text("Pieces");
                        header.Cell().Element(CellStyle).Text("COD Amount");

                        // you can extend existing styles by creating additional methods
                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                    });
                    int i = 0;
                    foreach (var page in Model.Details)
                    {
                        i++;
                        table.Cell().Element(CellStyle).ExtendHorizontal().AlignLeft().Text($"{i}");

                        // inches
                        table.Cell().Element(CellStyle).Text(page.OrderNumber);
                        table.Cell().Element(CellStyle).Text(page.TrackingNumber);

                        // points
                        table.Cell().Element(CellStyle).Text(page.City);
                        table.Cell().Element(CellStyle).Text(page.PaymentMethod);
                        table.Cell().Element(CellStyle).Text($"{page.TotalQuantity}");
                        table.Cell().Element(CellStyle).Text($"{page.TotalAmount}");

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
                    }
                });
        }
    }
}
