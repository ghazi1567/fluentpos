using FluentPOS.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Invoicing
{
    public interface IShareConfirmationService
    {
        Task<bool> UpdateConfirmation(WhatsappEvent waEvent);

    }
}
