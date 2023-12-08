﻿using FluentPOS.Modules.Invoicing.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Services
{
    public interface ISyncService
    {
        Task<List<PurchaseOrder>> GetPendingPurchaseOrdersAsync(long clientId);

        Task<bool> UpdateLogs(SyncLog syncLog);

        Task<List<InternalOrder>> GetPendingStockInAsync(long clientId);

        Task<List<InternalOrder>> GetPendingStockOutAsync(long clientId);
    }
}