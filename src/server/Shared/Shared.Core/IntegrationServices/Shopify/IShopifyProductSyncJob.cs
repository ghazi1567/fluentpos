﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Shopify
{
    public interface IShopifyProductSyncJob
    {
        string SyncShopifyProducts();
    }
}
