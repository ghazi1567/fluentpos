using BarcodeStandard;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

namespace FluentPOS.Modules.Invoicing.Core.Dtos.Prints
{
    public class OrderInvoiceDocument : IDocument
    {

        public OrderResponseDto Model { get; }

        public OrderInvoiceDocument(OrderResponseDto model)
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
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.ExtendLastCellsToTableBottom();

                    table.Cell().RowSpan(3).ScaleToFit().Padding(10).Image(LogoImage);

                    table.Cell().LabelCell("Time");
                    table.Cell().ValueCell("xxxxx");
                    table.Cell().RowSpan(3).Component(new BarcodeComponent(Model.TrackingNumber));
                    table.Cell().LabelCell("Date");
                    table.Cell().ValueCell(Model.ProcessedAt.Value.Date.ToString("dd MMM yyyy"));

                    table.Cell().ColumnSpan(2).LabelCell("Inspector");
                    table.Cell().ColumnSpan(2).Component(new AddressComponent("CONSIGNEE", Model));
                    table.Cell().ColumnSpan(2).Component(new AddressComponent("SHIPPER", Model));

                    table.Cell().ValueCell("WEIGHT:");
                    table.Cell().ValueCell("7:13");

                    table.Cell().ValueCell("PIECES");
                    table.Cell().ValueCell("18:25");

                    table.Cell().ValueCell("NO OF ITEMS:");
                    table.Cell().ValueCell("Sunny");

                    table.Cell().ValueCell("FRAGILE");
                    table.Cell().ValueCell("Windy");

                    table.Cell().ValueCell("Product Detail");
                    table.Cell().ColumnSpan(2).ValueCell("Mild");

                    table.Cell().RowSpan(2).ValueCell("Wind");

                    table.Cell().ValueCell("REMARKS:");
                    table.Cell().ColumnSpan(2).ValueCell("32°C");

                    table.Cell().LabelCell("Remarks");
                    table.Cell().ColumnSpan(3).ValueCell("Miniso Pakistan offers 7 days refund policy if the received product is defective or damaged.\r\n\r\nIncase of any complaint, please call at: +92 311 111 1600 or send email at: support.pk@miniso.com");
                });
        }
    }

    public static class SimpleExtension
    {
        private static IContainer Cell(this IContainer container, bool dark)
        {
            return container
                .Border(1)
                .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
                .Padding(3);
        }

        // displays only text label
        public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();

        // allows you to inject any type of content, e.g. image
        public static IContainer ValueCell(this IContainer container) => container.Cell(false);

        public static void ValueCell(this IContainer container, string text) => container.Cell(false).Text(text);

    }

    public class AddressComponent : IComponent
    {
        private string Title { get; }

        private OrderResponseDto Model { get; }

        public AddressComponent(string title, OrderResponseDto model)
        {
            Title = title;
            Model = model;
        }

        public void Compose(IContainer container)
        {
            container.Border(1).Padding(5).ShowEntire().Column(column =>
            {
                column.Item().Text(Title).SemiBold();
                column.Item().PaddingBottom(2);

                column.Item().Text(Model.FirstName);
                column.Item().Text(Model.Address1);
                column.Item().Text($"{Model.City}, {Model.Country}");
                column.Item().Text(Model.Phone);
            });
        }
    }

    public class BarcodeComponent : IComponent
    {
        private string BarcodeString { get; }

        private Image BarcodeImage { get; }

        public BarcodeComponent(string barocde)
        {
            BarcodeString = barocde;
            if (!string.IsNullOrEmpty(barocde))
            {
                var b = new Barcode();
                b.Encode(BarcodeStandard.Type.Code128, barocde, 290, 50);
                BarcodeImage = Image.FromBinaryData(b.EncodedImageBytes);
            }
        }

        public void Compose(IContainer container)
        {
            container.Border(1).Padding(10).ShowEntire().Column(column =>
            {
                column.Spacing(2);
                if (!string.IsNullOrEmpty(BarcodeString))
                {
                    column.Item().Image(BarcodeImage);
                    column.Item().AlignCenter().Text(BarcodeString);
                    column.Item().PaddingBottom(5);
                }
                else
                {
                    column.Item().AlignCenter().Text("Tracking not generated.");
                }
            });
        }
    }
}
