using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Services
{
    public interface IOrderLogger
    {
        void LogInfo(long shopifyId, string detail);


        void LogInfo(long orderId, long foId, string detail);

        void LogInfo(long shopifyId, long? orderId, string detail);

        void LogInfo(long shopifyId, long orderId, long foId, string detail);

        void AssignedToWarehouse(long orderId, long foId, long WarehouseId);

        Task<List<long>> GetLastAssignedWarehouse(long foId);

        Task MarkAsIgnoreAsync(long foId);
    }

    public class OrderLogger : IOrderLogger
    {
        private readonly ISalesDbContext _context;
        private readonly ILogger<OrderLogger> _logger;

        public OrderLogger(ISalesDbContext context, ILogger<OrderLogger> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<long>> GetLastAssignedWarehouse(long foId)
        {
            return await _context.OrderLogs.AsNoTracking().Where(x => x.FulfillmentOrderId == foId && x.WarehouseId > 0 && x.Ignore == false).Select(x => x.WarehouseId).ToListAsync();
        }

        public void AssignedToWarehouse(long orderId, long foId, long WarehouseId)
        {
            var log = new OrderLogs
            {
                InternalOrderId = orderId,
                FulfillmentOrderId = foId,
                WarehouseId = WarehouseId,
                LogDescription = $"Assigned to warehouse = {WarehouseId}"
            };
            SaveLog(log);
        }

        public async Task MarkAsIgnoreAsync(long foId)
        {
            var list = await _context.OrderLogs.AsNoTracking().Where(x => x.FulfillmentOrderId == foId && x.WarehouseId > 0 && x.Ignore == false).ToListAsync();
            foreach (var item in list)
            {
                item.Ignore = true;
            }

            _context.OrderLogs.UpdateRange(list);
            await _context.SaveChangesAsync();
        }

        public void LogInfo(long shopifyId, string detail)
        {
            var log = new OrderLogs
            {
                ShopifyId = shopifyId,
                LogDescription = string.Format(detail, DateTime.Now, shopifyId)
            };
            SaveLog(log);
        }

        public void LogInfo(long shopifyId, long? orderId, string detail)
        {
            var log = new OrderLogs
            {
                ShopifyId = shopifyId,
                InternalOrderId = orderId.HasValue ? orderId.Value : 0,
                LogDescription = string.Format(detail, DateTime.Now, shopifyId)
            };
            SaveLog(log);
        }


        public void LogInfo(long orderId, long foId, string detail)
        {
            var log = new OrderLogs
            {
                InternalOrderId = orderId,
                FulfillmentOrderId = foId,
                LogDescription = string.Format(detail, DateTime.Now, orderId)
            };
            SaveLog(log);
        }

        public void LogInfo(long shopifyId, long orderId, long foId, string detail)
        {
            var log = new OrderLogs
            {
                ShopifyId = shopifyId,
                InternalOrderId = orderId,
                FulfillmentOrderId = foId,
                LogDescription = string.Format(detail, DateTime.Now, shopifyId)
            };
            SaveLog(log);
        }

        public void SaveLog(OrderLogs orderLogs)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(orderLogs));
            _context.OrderLogs.Add(orderLogs);
        }
    }
}
