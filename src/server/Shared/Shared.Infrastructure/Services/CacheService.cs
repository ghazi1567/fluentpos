using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IJsonSerializer _jsonSerializer;

        public CacheService(
            IDistributedCache cache,
            IJsonSerializer jsonSerializer)
        {
            _cache = cache;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<TResponse> GetAsync<TResponse>(string key)
        {
            byte[] cachedResponse = !string.IsNullOrWhiteSpace(key) ? await _cache.GetAsync(key, default(CancellationToken)) : null;
            return _jsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));
        }

        public async Task<TResponse> SetAsync<TResponse>(string key, TResponse response)
        {
            //var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromHours(_settings.SlidingExpiration);
            //if (slidingExpiration <= TimeSpan.Zero)
            //{
            //    throw new CustomException(_localizer["Cache Sliding Expiration must be greater than 0."], statusCode: HttpStatusCode.BadRequest);
            //}

            var options = new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromHours(2) };
            byte[] serializedData = Encoding.Default.GetBytes(_jsonSerializer.Serialize(response));
            await _cache.SetAsync(key, serializedData, options, default(CancellationToken));
            return response;
        }
    }
}