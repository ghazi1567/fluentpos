using FluentPOS.Shared.DTOs.Sales.Orders;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using ShopifySharp;
using ShopifySharp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Twilio.Rest.Trunking.V1;

namespace FluentPOS.Shared.Infrastructure.Services.Shopify
{
    public class ShopifyServiceExtended : ShopifyService
    {
        private string _myShopifyUrl;
        private string _shopAccessToken;

        public ShopifyServiceExtended(string myShopifyUrl, string shopAccessToken)
            : base(myShopifyUrl, shopAccessToken)
        {
            _myShopifyUrl = myShopifyUrl;
            _shopAccessToken = shopAccessToken;
        }

        public virtual async Task<RequestResult<FulfillmentOrderMove>> MoveAsync(long fulfillmentOrderId, SplitOrderPayloadDto splitOrderPayloadDto)
        {
            var content = new JsonContent(splitOrderPayloadDto);
            var req = PrepareRequest($"fulfillment_orders/{fulfillmentOrderId}/move.json");
            return await ExecuteRequestAsync<FulfillmentOrderMove>(req, HttpMethod.Post, default, content);
        }


        public async Task<SplitOrderGraphqlResponse> SplitFulfillment(long fulfillmentOrderId, SplitOrderPayloadDto splitOrderPayloadDto)
        {

            var graphQLHttpClientOptions = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri($"{_myShopifyUrl}/admin/api/2023-10/graphql.json")
            };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Shopify-Access-Token", _shopAccessToken);
            var graphQLClient = new GraphQLHttpClient(graphQLHttpClientOptions, new NewtonsoftJsonSerializer(), httpClient);

            var payload = new List<SplitOrderFOGQPayloadDto>
            {
                new SplitOrderFOGQPayloadDto
                {
                    fulfillmentOrderId = $"gid://shopify/FulfillmentOrder/{fulfillmentOrderId}",
                    fulfillmentOrderLineItems = splitOrderPayloadDto.fulfillment_order.fulfillment_order_line_items.Select(x => new SplitOrderFOLineItemGQDto
                    {
                        id = $"gid://shopify/FulfillmentOrderLineItem/{x.id}",
                        quantity = x.quantity
                    }).ToList()
                }
            };

            var movieRequest = new GraphQLRequest
            {
                Query = @"
                    mutation fulfillmentOrderSplit($fulfillmentOrderSplits: [FulfillmentOrderSplitInput!]!) {
                      fulfillmentOrderSplit(fulfillmentOrderSplits: $fulfillmentOrderSplits) {
                        fulfillmentOrderSplits {
                          fulfillmentOrder {
                            id
                            __typename
                          }
                          remainingFulfillmentOrder {
                            id
                            __typename
                          }
                        }
                        userErrors {
                          field
                          message
                        }
                      }
                    }
                ",
                Variables = new
                {
                    fulfillmentOrderSplits = payload
                }
            };

            var graphQLResponse = await graphQLClient.SendQueryAsync<SplitOrderGraphqlResponse>(movieRequest);
            return graphQLResponse.Data;
        }
    }
}
