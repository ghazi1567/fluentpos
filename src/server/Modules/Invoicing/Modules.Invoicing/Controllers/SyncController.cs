using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Services;
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

        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpGet("NewPurchaseOrder/{clientId}")]
        public async Task<IActionResult> GetPendingPurchaseOrdersAsync(Guid clientId)
        {
            return Ok(await _syncService.GetPendingPurchaseOrdersAsync(clientId));
        }

        [HttpGet("NewStockIn/{clientId}")]
        public async Task<IActionResult> GetStockInAsync(Guid clientId)
        {
            return Ok(await _syncService.GetPendingStockInAsync(clientId));
        }

        [HttpGet("NewStockOut/{clientId}")]
        public async Task<IActionResult> GetStockOutAsync(Guid clientId)
        {
            return Ok(await _syncService.GetPendingStockOutAsync(clientId));
        }

        [HttpPost("UpdateLogs")]
        public async Task<IActionResult> UpdateLogs(SyncLog syncLog)
        {
            return Ok(await _syncService.UpdateLogs(syncLog));
        }
    }
}