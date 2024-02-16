using FluentPOS.Modules.Invoicing.Core.Entities;
using FluentPOS.Shared.Core.IntegrationServices.Invoicing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Core.Services.Confirmation
{
    public interface IConfirmationService 
    {
        Task<bool> WhatsAppConfirmation(InternalFulfillmentOrder order);

    }
}
