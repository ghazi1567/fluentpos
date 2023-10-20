using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Features.Products.Commands;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Core.IntegrationServices.Shopify;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShopifySharp.Filters;
using ShopifySharp.Lists;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Infrastructure.Services
{
    public class ShopifyProductSyncJob : HangfireService, IShopifyProductSyncJob
    {
        private readonly IHttpContextAccessor _accessor;

        public readonly string StoreId;
        public readonly string _shopifyUrl;

        public readonly string _accessToken;
        public readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ShopifyProductSyncJob(IHttpContextAccessor accessor, IProductService productService, IMapper mapper, IMediator mediator)
        {
            _accessor = accessor;
            _productService = productService;
            _mapper = mapper;
            _mediator = mediator;

            StoreId = _accessor.HttpContext?.Request?.Headers["store-id"];
            if (!string.IsNullOrEmpty(StoreId))
            {
                string shopifyCreds = EncryptionUtilities.DecryptString(StoreId);
                _shopifyUrl = shopifyCreds.Split("|")[0];
                _accessToken = shopifyCreds.Split("|")[1];
            }
        }

        public async Task<bool> FetchAndSaveShopifyPoroducts(string shopifyUrl, string accessToken, ListFilter<ShopifySharp.Product> filter = null)
        {
            var service = new ShopifySharp.ProductService(shopifyUrl, accessToken);
            ListResult<ShopifySharp.Product> products = null;

            if (filter != null)
            {
                products = await service.ListAsync(filter);
            }
            else
            {

                products = await service.ListAsync();
            }


            await SaveProducts(products);

            if (products.HasNextPage)
            {
                await FetchAndSaveShopifyPoroducts(shopifyUrl, accessToken, products.GetNextPageFilter());
            }

            return true;
        }



        public string SyncShopifyProducts()
        {
            return Enqueue(() => FetchAndSaveShopifyPoroducts(_shopifyUrl, _accessToken, null));
        }

        public async Task SaveProducts(ListResult<ShopifySharp.Product> products)
        {
            if (products.Items.Count() > 0)
            {
                foreach (var shopifyProduct in products.Items)
                {
                    try
                    {
                        var product = _mapper.Map<RegisterProductCommand>(shopifyProduct);
                        var response = await _mediator.Send(product);

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
