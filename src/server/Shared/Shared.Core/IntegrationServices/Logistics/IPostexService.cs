using FluentPOS.Shared.DTOs.Dtos.Logistics;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Logistics
{
    public interface IPostexService
    {
        Task<PostexResponseModel> GenerateCNAsync(PostexOrderModel postexOrderModel);

        Task<PostexResponseModel> GenerateLoadsheetAsync(PostexLoadSheetModel model);
    }
}
