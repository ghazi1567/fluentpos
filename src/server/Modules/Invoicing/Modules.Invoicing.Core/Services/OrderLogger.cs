using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace FluentPOS.Modules.Invoicing.Core.Services
{
    public interface IOrderLogger
    {
        void LogInfo(long shopifyId, string detail);

        void LogInfo(Guid orderId, string detail);

        void LogInfo(Guid orderId, Guid foId, string detail);

        void LogInfo(long shopifyId, Guid? orderId, string detail);

        void LogInfo(long shopifyId, Guid orderId, Guid foId, string detail);
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

        public void LogInfo(long shopifyId, string detail)
        {
            var log = new OrderLogs
            {
                ShopifyId = shopifyId,
                LogDescription = string.Format(detail, DateTime.Now, shopifyId)
            };
            SaveLog(log);
        }

        public void LogInfo(long shopifyId, Guid? orderId, string detail)
        {
            var log = new OrderLogs
            {
                ShopifyId = shopifyId,
                InternalOrderId = orderId.HasValue ? orderId.Value : Guid.Empty,
                LogDescription = string.Format(detail, DateTime.Now, shopifyId)
            };
            SaveLog(log);
        }

        public void LogInfo(Guid orderId, string detail)
        {
            var log = new OrderLogs
            {
                InternalOrderId = orderId,
                LogDescription = string.Format(detail, DateTime.Now, orderId)
            };
            SaveLog(log);
        }

        public void LogInfo(Guid orderId, Guid foId, string detail)
        {
            var log = new OrderLogs
            {
                InternalOrderId = orderId,
                FulfillmentOrderId = foId,
                LogDescription = string.Format(detail, DateTime.Now, orderId)
            };
            SaveLog(log);
        }

        public void LogInfo(long shopifyId, Guid orderId, Guid foId, string detail)
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
