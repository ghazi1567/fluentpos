using AutoMapper;
using FluentPOS.Modules.Invoicing.Core.Features.Warehouses.Commands;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShopifySharp;
using ShopifySharp.Filters;
using ShopifySharp.Lists;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Invoicing.Infrastructure.Services
{
    public class ShopifyLocationService : IShopifyLocationService
    {
        private readonly IHttpContextAccessor _accessor;

        public readonly string StoreId;
        public readonly string _shopifyUrl;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public readonly string _accessToken;
        public readonly LocationService _locationService;

        public ShopifyLocationService(IStoreService storeService, IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            string shopifyCreds = storeService.StoreId();
            _shopifyUrl = shopifyCreds.Split("|")[0];
            _accessToken = shopifyCreds.Split("|")[1];

            if (!string.IsNullOrEmpty(_shopifyUrl) && !string.IsNullOrEmpty(_accessToken))
            {
                _locationService = new LocationService(_shopifyUrl, _accessToken);
            }
            else
            {
                throw new System.Exception("Store url or access token missing");
            }
        }

        public async Task<bool> SyncLocations()
        {
            return await GetLocations();
        }

        public async Task<bool> GetLocations(ListFilter<Location> filter = null)
        {
            try
            {
                ListResult<Location> locations = null;
                if (filter == null)
                {
                    locations = await _locationService.ListAsync();
                }
                else
                {
                    locations = await _locationService.ListAsync(filter);
                }

                if (locations.HasNextPage)
                {
                    await GetLocations(locations.GetNextPageFilter());
                }

                await SaveLocations(locations);
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task SaveLocations(ListResult<Location> locations)
        {
            if (locations.Items.Count() > 0)
            {
                foreach (var location in locations.Items)
                {
                    try
                    {
                        var order = _mapper.Map<RegisterLocationCommand>(location);
                        var response = await _mediator.Send(order);
                        Console.WriteLine(response.Messages);
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}