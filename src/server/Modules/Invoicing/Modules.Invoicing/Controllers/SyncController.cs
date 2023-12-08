using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Services;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Controllers
{
    [ApiVersion("1")]
    internal class SyncController : BaseController
    {
        private readonly ISyncService _syncService;
        private readonly IShopifyOrderSyncJob _shopifyOrderSyncJob;

        public SyncController(ISyncService syncService, IShopifyOrderSyncJob shopifyOrderSyncJob)
        {
            _syncService = syncService;
            _shopifyOrderSyncJob = shopifyOrderSyncJob;
        }

        [HttpGet("NewPurchaseOrder/{clientId}")]
        public async Task<IActionResult> GetPendingPurchaseOrdersAsync(long clientId)
        {
            return Ok(await _syncService.GetPendingPurchaseOrdersAsync(clientId));
        }

        [HttpGet("NewStockIn/{clientId}")]
        public async Task<IActionResult> GetStockInAsync(long clientId)
        {
            return Ok(await _syncService.GetPendingStockInAsync(clientId));
        }

        [HttpGet("NewStockOut/{clientId}")]
        public async Task<IActionResult> GetStockOutAsync(long clientId)
        {
            return Ok(await _syncService.GetPendingStockOutAsync(clientId));
        }

        [HttpPost("UpdateLogs")]
        public async Task<IActionResult> UpdateLogs(SyncLog syncLog)
        {
            return Ok(await _syncService.UpdateLogs(syncLog));
        }

        [HttpGet("SyncOrders")]
        public IActionResult SyncOrders()
        {
            return Ok(_shopifyOrderSyncJob.RunOrderWebhook());
        }
    }
}