using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Abstractions;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.DTOs.Sales.Enums;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Invoicing.Core.Services
{
    public class SyncService : ISyncService
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;

        public SyncService(
            ISalesDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<PurchaseOrder>> GetPendingPurchaseOrdersAsync(long clientId)
        {
            return await _context.PurchaseOrders
                .Where(x => _context.SyncLogs.Any(s => s.EntryId == x.Id && s.RemoteClientId == clientId && s.EntryType == "PurchaseOrder") == false)
                .AsNoTracking()
                .Include(x => x.Products)
                .OrderBy(x => x.TimeStamp)
                .ToListAsync();
        }

        public async Task<List<InternalOrder>> GetPendingStockInAsync(long clientId)
        {
            return await _context.Orders
                .Where(x => _context.SyncLogs.Any(s => s.EntryId == x.Id && s.RemoteClientId == clientId && s.EntryType == "StockIn") == false
                 && x.Status == OrderStatus.Pending) // && x.OrderType == OrderType.StockIn)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<InternalOrder>> GetPendingStockOutAsync(long clientId)
        {
            return await _context.Orders
                .Where(x => _context.SyncLogs.Any(s => s.EntryId == x.Id && s.RemoteClientId == clientId && s.EntryType == "StockOut") == false)
                //&& x.OrderType == OrderType.StockOut)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateLogs(SyncLog syncLog)
        {

            var log = _context.SyncLogs.SingleOrDefault(x => x.EntryId == syncLog.EntryId && x.EntryType == syncLog.EntryType && x.RemoteClientId == syncLog.RemoteClientId);
            if (log == null)
            {
                await _context.SyncLogs.AddAsync(syncLog);
            }
            else
            {
                log.UpdatedAt = DateTime.Now;
                log.LastUpdateOn = DateTime.Now;
                _context.SyncLogs.Update(log);

            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLogStatus(long entryId, string entryType, string status)
        {

            var log = _context.SyncLogs.SingleOrDefault(x => x.EntryId == entryId && x.EntryType == entryType);
            if (log != null)
            {
                log.UpdatedAt = DateTime.Now;
                log.LastUpdateOn = DateTime.Now;
                log.Status = status;
                _context.SyncLogs.Update(log);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}