using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Services.BackgroundJob
{
    public abstract class BaseTimerBackgroundTask : IHostedService, IDisposable
    {
        #region Properties and Data Members

        private Timer Timer { get; set; }

        private bool Processing { get; set; }

        protected int Frequency { get; set; }

        protected bool Enabled { get; set; }

        protected TimerCallback ProcessAction { get; set; }
        #endregion


        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.Timer = new Timer(this.TimerCallbackHandler, null, 0, this.Frequency);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            this.Timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        #region Private Methods

        /// <summary>
        /// TimerCallbackHandler calls AuctionApplication layer for Status updates.
        /// </summary>
        /// <param name="state"></param>
        protected void TimerCallbackHandler(object state)
        {
            try
            {
                if (this.Processing == false)
                {
                    this.Processing = true;

                    this.ProcessAction(state);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.Processing = false;
            }
        }
        #endregion

        public void Dispose()
        {
            this.Timer?.Dispose();
        }
    }
}