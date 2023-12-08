using System;

namespace FluentPOS.Shared.DTOs.Sales.Orders
{
    public record ProductResponse
    (
        long OrderId,
        long ProductId,
        int Quantity,
        string Category,
        string Brand,
        decimal Price,
        decimal Tax,
        decimal Discount,
        decimal Total,
        string productName = "",
        int OrderedQuantity = 0
    );

    public record StockInProductResponse
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string productName { get; set; }
        public string BarcodeSymbology { get; set; }
        public int OrderedQuantity { get; set; }
        public bool IsNotPO { get; set; }
    }
}