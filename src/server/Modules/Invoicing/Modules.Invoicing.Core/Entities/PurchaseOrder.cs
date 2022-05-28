using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Sales.Enums;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Invoicing.Core.Entities
{

    public class PurchaseOrder : BaseEntity
    {
        public string ReferenceNumber { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal Total { get; set; }

        public OrderStatus Status { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? ApproveDate { get; set; }

        public Guid ApproveBy { get; set; }

        public string Note { get; private set; }

        public Guid WarehouseId { get; private set; }

        public virtual ICollection<POProduct> Products { get; private set; } = new List<POProduct>();

        public static PurchaseOrder InitializeOrder()
        {
            return new PurchaseOrder { TimeStamp = DateTime.Now };
        }

        public static PurchaseOrder InitializeOrder(DateTime dateTime)
        {
            return new PurchaseOrder { TimeStamp = dateTime };
        }

        public void SetReferenceNumber(string referenceNumber)
        {
            ReferenceNumber = referenceNumber;
        }

        public void SetWarehouseId(Guid warehouseId)
        {
            WarehouseId = warehouseId;
        }

        public void SetNotes(string notes)
        {
            Note = notes;
        }

        public void AddProduct(POProduct product)
        {
            Products.Add(product);
        }

        internal void AddProduct(Guid productId, string name, int quantity, decimal rate, decimal tax)
        {
            Products.Add(new POProduct
            {
                ProductId = productId,
                Quantity = quantity,
                Tax = tax * quantity,
                Price = quantity * rate,
                Total = (quantity * rate) + (tax * quantity)
            });
        }
    }
}