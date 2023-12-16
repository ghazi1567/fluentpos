using FluentPOS.Modules.Inventory.Core.Features.Levels;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Infrastructure.Services;
using MediatR;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Infrastructure.Services
{
    public class ShopifyInventoryJob : HangfireService, IShopifyInventoryJob
    {
        private readonly IMediator _mediator;

        public ShopifyInventoryJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string ProcessInventory()
        {
            return Enqueue(() => FetchAndProcessInventory());
        }

        public async Task<bool> FetchAndProcessInventory()
        {
            var result = await _mediator.Send(new UpdateInventoryCommand());
            return result.Succeeded;
        }
    }
}
