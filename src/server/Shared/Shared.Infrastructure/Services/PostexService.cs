using FluentPOS.Shared.Core.IntegrationServices.Logistics;
using FluentPOS.Shared.DTOs.Dtos.Logistics;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class PostexService : IPostexService
    {
        public async Task<PostexResponseModel> GenerateCNAsync(PostexOrderModel postexOrderModel)
        {
            PostexResponseModel responseModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stg-api.postex.pk/services/integration/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", "YzE1ZjcxYjE1MDQ5NDY3ZDk4NjUxNzRhMTM3MmMxODc6YTgyYTFmODBiYmY1NGQyODg1YTU2YTY0YzRjMmMyMjE=");


                HttpResponseMessage response = await client.PostAsJsonAsync("api/order/v2/create-order", postexOrderModel);
                if (response.IsSuccessStatusCode)
                {
                    responseModel = await response.Content.ReadFromJsonAsync<PostexResponseModel>();
                }
                else
                {
                    responseModel = await response.Content.ReadFromJsonAsync<PostexResponseModel>();
                }
            }

            return responseModel;
        }

        public async Task<PostexResponseModel> GenerateLoadsheetAsync(PostexLoadSheetModel model)
        {
            PostexResponseModel responseModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stg-api.postex.pk/services/integration/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", "YzE1ZjcxYjE1MDQ5NDY3ZDk4NjUxNzRhMTM3MmMxODc6YTgyYTFmODBiYmY1NGQyODg1YTU2YTY0YzRjMmMyMjE=");


                HttpResponseMessage response = await client.PostAsJsonAsync("api/order/v2/generate-load-sheet", model);
                if (response.IsSuccessStatusCode)
                {
                    responseModel = new PostexResponseModel
                    {
                        StatusCode = "200"
                    };
                }
                else
                {
                    responseModel = await response.Content.ReadFromJsonAsync<PostexResponseModel>();
                }
            }

            return responseModel;
        }
    }
}