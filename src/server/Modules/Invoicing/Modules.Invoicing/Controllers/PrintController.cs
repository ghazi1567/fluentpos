using FluentPOS.Modules.Invoicing.Core.Dtos.Prints;
using FluentPOS.Modules.Invoicing.Core.Features.Sales.Queries;
using FluentPOS.Modules.Invoicing.Core.Services;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class PrintController : BaseController
    {
        private readonly ISyncService _syncService;
        private readonly IShopifyOrderSyncJob _shopifyOrderSyncJob;

        public PrintController(ISyncService syncService, IShopifyOrderSyncJob shopifyOrderSyncJob)
        {
            _syncService = syncService;
            _shopifyOrderSyncJob = shopifyOrderSyncJob;
        }

        [AllowAnonymous]
        [HttpGet("OrderPrint/{id}")]
        public async Task<IActionResult> OrderPrint(long id)
        {
            var orderDetail = await Mediator.Send(new GetFOByIdQuery { Id = id, WarehouseIds = new long[0] });
            Settings.License = LicenseType.Community;
            var document = new OrderInvoiceDocument(orderDetail.Data);
            byte[] pdfBytes = document.GeneratePdf();
            MemoryStream ms = new MemoryStream(pdfBytes);
            return new FileStreamResult(ms, "application/pdf");
        }

        [AllowAnonymous]
        [HttpGet("Loadsheet/{id}")]
        public async Task<IActionResult> Loadsheet(long id)
        {
            var orderDetail = await Mediator.Send(new GetLoadsheetInByIdQuery { Id = id });
            Settings.License = LicenseType.Community;
            var document = new LoadSheetDocument(orderDetail.Data);
            byte[] pdfBytes = document.GeneratePdf();
            MemoryStream ms = new MemoryStream(pdfBytes);
            return new FileStreamResult(ms, "application/pdf");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProcessWebhook()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            // code in your main method
            Document document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content()
                    .MinimalBox()
                        .Border(1)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100);
                                columns.RelativeColumn();
                                columns.ConstantColumn(100);
                                columns.RelativeColumn();
                            });

                            table.ExtendLastCellsToTableBottom();

                            table.Cell().RowSpan(3).LabelCell("Project");
                            table.Cell().RowSpan(3).ShowEntire().ValueCell(Placeholders.Sentence());

                            table.Cell().LabelCell("Report number");
                            table.Cell().ValueCell("asda");

                            table.Cell().LabelCell("Date");
                            table.Cell().ValueCell(Placeholders.ShortDate());

                            table.Cell().LabelCell("Inspector");
                            table.Cell().ValueCell("Marcin Ziąbek");

                            table.Cell().ColumnSpan(2).LabelCell("Morning weather");
                            table.Cell().ColumnSpan(2).LabelCell("Evening weather");

                            table.Cell().ValueCell("Time");
                            table.Cell().ValueCell("7:13");

                            table.Cell().ValueCell("Time");
                            table.Cell().ValueCell("18:25");

                            table.Cell().ValueCell("Description");
                            table.Cell().ValueCell("Sunny");

                            table.Cell().ValueCell("Description");
                            table.Cell().ValueCell("Windy");

                            table.Cell().ValueCell("Wind");
                            table.Cell().ValueCell("Mild");

                            table.Cell().ValueCell("Wind");
                            table.Cell().ValueCell("Strong");

                            table.Cell().ValueCell("Temperature");
                            table.Cell().ValueCell("17°C");

                            table.Cell().ValueCell("Temperature");
                            table.Cell().ValueCell("32°C");

                            table.Cell().LabelCell("Remarks");
                            table.Cell().ColumnSpan(3).ValueCell(Placeholders.Paragraph());
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });
            byte[] pdfBytes = document.GeneratePdf();
            MemoryStream ms = new MemoryStream(pdfBytes);
            return new FileStreamResult(ms, "application/pdf");
        }

        [AllowAnonymous]
        [HttpGet("LoadsheetTest")]
        public async Task<IActionResult> GetLoadSheet()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            const int inchesToPoints = 72;
            var pageSizes = new List<(string sr, string ordeerNo, string CN, string city, string PaymenMethod, string pieces, string amount)>()
{
    ("1","Bee1051", "1236540","Lahore","COD","2","10000"),
     ("1","Bee1051", "1236540","Lahore","COD","2","10000"),
      ("1","Bee1051", "1236540","Lahore","COD","2","10000"),
       ("1","Bee1051", "1236540","Lahore","COD","2","10000"),
        ("1","Bee1051", "1236540","Lahore","COD","2","10000"),
         ("1","Bee1051", "1236540","Lahore","COD","2","10000"),
};

            // code in your main method
            Document document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content()
                    .Padding(10)
                    .MinimalBox()
                    .Border(1)
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

    IContainer TopHeaderCellStyle(IContainer container, string backgroundColor)
    {
        return container
            .Border(0)
            .BorderColor(Colors.White)
            .Background(backgroundColor)
            .PaddingVertical(5)
            .PaddingHorizontal(10)
            .AlignCenter()
            .AlignMiddle()
            .Height(50);
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
        header.Cell().ColumnSpan(2).Element(TopHeaderStyle).Text("Miniso");
        header.Cell().ColumnSpan(2).Element(TopHeaderStyle).Text("Postex");
        header.Cell().ColumnSpan(2).Element(TopHeaderStyle).Text("Printed By");
        header.Cell().Element(TopHeaderStyle).Text("generated at");
        header.Cell().Element(CellStyle).Text("Sr#");
        header.Cell().Element(CellStyle).Text("ORDER NO.");
        header.Cell().Element(CellStyle).Text("CN");
        header.Cell().Element(CellStyle).Text("CITY");

        header.Cell().Element(CellStyle).Text("PAYMENT METHOD");
        header.Cell().Element(CellStyle).Text("PIECES");
        header.Cell().Element(CellStyle).Text("COD AMOUNT");

        // you can extend existing styles by creating additional methods
        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
        IContainer TopHeaderStyle(IContainer container) => TopHeaderCellStyle(container, Colors.White);
    });

    foreach (var page in pageSizes)
    {
        table.Cell().Element(CellStyle).Text(page.sr);
        table.Cell().Element(CellStyle).Text(page.ordeerNo);

        // inches
        table.Cell().Element(CellStyle).Text(page.CN);
        table.Cell().Element(CellStyle).Text(page.city);
        table.Cell().Element(CellStyle).Text(page.PaymenMethod);

        // points
        table.Cell().Element(CellStyle).Text(page.pieces);
        table.Cell().Element(CellStyle).Text(page.amount);

        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
    }
});
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });
            byte[] pdfBytes = document.GeneratePdf();
            MemoryStream ms = new MemoryStream(pdfBytes);
            return new FileStreamResult(ms, "application/pdf");
        }

    }

    public static class SimpleExtension
    {
        private static IContainer Cell(this IContainer container, bool dark)
        {
            return container
                .Border(1)
                .Background(dark ? Colors.Grey.Lighten2 : Colors.White)
                .Padding(10);
        }

        // displays only text label
        public static void LabelCell(this IContainer container, string text) => container.Cell(true).Text(text).Medium();

        // allows you to inject any type of content, e.g. image
        public static IContainer ValueCell(this IContainer container) => container.Cell(false);

        public static void ValueCell(this IContainer container, string text) => container.Cell(false).Text(text);

    }
}