using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Application
{
    public interface ICacheService
    {
        Task<TResponse> GetAsync<TResponse>(string key);

        Task<TResponse> SetAsync<TResponse>(string key, TResponse response);
    }
}
