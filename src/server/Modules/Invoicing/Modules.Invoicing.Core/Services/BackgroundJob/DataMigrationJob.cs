using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Services.BackgroundJob
{
    public class DataMigrationJob : BaseTimerBackgroundTask
    {


        public DataMigrationJob(
            IServiceProvider serviceProvider,
            IConfiguration config)
        {
            _dMSEndpointUrl = config.GetValue<string>("BackgroudJobs:UploadDocument:DMSEndpointUrl");

            var frequency = config.GetValue<int>("BackgroudJobs:UploadDocument:Frequency");

            if (Frequency == 0)
            {
                Frequency = 150000; //15 min
            }

            Frequency = Frequency;

            ProcessAction = this.UploadDocuments;

            _serviceProvider = serviceProvider;
        }


        #region Properties and Data Members
        private readonly IServiceProvider _serviceProvider;
        private bool _taskLock = false;
        private string _dMSEndpointUrl = string.Empty;
        #endregion

        #region Private Methods
        private async void UploadDocuments(object state)
        {

            if (_taskLock == false)
            {
                _taskLock = true;

                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                }

                _taskLock = false;
            }
        }

        public async Task<T> GetDMSUrl<T>(string url)
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


        #endregion 
    }
}
