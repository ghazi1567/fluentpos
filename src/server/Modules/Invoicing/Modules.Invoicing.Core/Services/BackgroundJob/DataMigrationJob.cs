using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Modules.Invoicing.Core.Features.PO.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Invoicing.Core.Services.BackgroundJob
{
    public class DataMigrationJob : BaseTimerBackgroundTask
    {


        public DataMigrationJob(
            IServiceProvider serviceProvider,
            IConfiguration config)
        {
            _enabled = config.GetValue<bool>("BackgroudJobs:DataMigration:Enabled");
            _url = config.GetValue<string>("BackgroudJobs:DataMigration:BaseUrl");
            _clientId = config.GetValue<Guid>("BackgroudJobs:DataMigration:ClientId");
            var frequency = config.GetValue<int>("BackgroudJobs:DataMigration:Frequency");

            if (Frequency == 0)
            {
                Frequency = 150000; //15 min
            }

            Frequency = Frequency;

            ProcessAction = this.Process;

            _serviceProvider = serviceProvider;
        }


        #region Properties and Data Members
        private readonly IServiceProvider _serviceProvider;
        private bool _taskLock = false;
        private string _url = string.Empty;
        private bool _enabled = false;
        private Guid _clientId = Guid.Empty;
        #endregion

        #region Private Methods

        private async void Process(object state)
        {
            if (_enabled && _taskLock == false)
            {
                _taskLock = true;

                await GetNewPurchaseOrder();
                await GetNewStockInOrder();
                await GetNewStockOutOrder();

                _taskLock = false;
            }
        }

        public async Task<T> GetApiData<T>(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseContent);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<bool> UpdateLogsApi(SyncLog syncLog)
        {
            try
            {
                string url = $"{_url}/UpdateLogs";
                using (var client = new HttpClient())
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(syncLog);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(responseContent);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task GetNewPurchaseOrder()
        {
            string url = $"{_url}/NewPurchaseOrder/{_clientId}";
            var purchaseOrders = await GetApiData<List<PurchaseOrder>>(url);
            if (purchaseOrders.Count > 0)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IPOService poService = scope.ServiceProvider.GetRequiredService<IPOService>();

                    foreach (var item in purchaseOrders)
                    {
                        bool result = true;

                        if (!await poService.AlreadyExist(item.Id))
                        {
                            result = await poService.SavePurchaseOrder(item);
                        }

                        if (result)
                        {
                            var logs = new SyncLog
                            {
                                CreatedAt = DateTime.Now,
                                EntryId = item.Id,
                                RemoteClientId = _clientId,
                                EntryType = "PurchaseOrder",
                                LastUpdateOn = DateTime.Now,
                            };
                            bool logResult = await UpdateLogsApi(logs);
                            if (!logResult)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private async Task GetNewStockInOrder()
        {
            string url = $"{_url}/NewStockIn/{_clientId}";
            var purchaseOrders = await GetApiData<List<InternalOrder>>(url);
            if (purchaseOrders.Count > 0)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IStockInService service = scope.ServiceProvider.GetRequiredService<IStockInService>();

                    foreach (var item in purchaseOrders)
                    {
                        bool result = true;

                        if (!await service.AlreadyExist(item.Id))
                        {
                            result = await service.SaveStockIn(item);
                        }

                        if (result)
                        {
                            var logs = new SyncLog
                            {
                                CreatedAt = DateTime.Now,
                                EntryId = item.Id,
                                RemoteClientId = _clientId,
                                EntryType = "StockIn",
                                LastUpdateOn = DateTime.Now,
                            };
                            bool logResult = await UpdateLogsApi(logs);
                            if (!logResult)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private async Task GetNewStockOutOrder()
        {
            string url = $"{_url}/NewStockOut/{_clientId}";
            var purchaseOrders = await GetApiData<List<InternalOrder>>(url);
            if (purchaseOrders.Count > 0)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IStockOutService service = scope.ServiceProvider.GetRequiredService<IStockOutService>();

                    foreach (var item in purchaseOrders)
                    {
                        bool result = true;

                        if (!await service.AlreadyExist(item.Id))
                        {
                            result = await service.Save(item);
                        }

                        if (result)
                        {
                            var logs = new SyncLog
                            {
                                CreatedAt = DateTime.Now,
                                EntryId = item.Id,
                                RemoteClientId = _clientId,
                                EntryType = "StockOut",
                                LastUpdateOn = DateTime.Now,
                            };
                            bool logResult = await UpdateLogsApi(logs);
                            if (!logResult)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}