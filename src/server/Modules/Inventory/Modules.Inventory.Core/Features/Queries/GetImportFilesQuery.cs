using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;

namespace FluentPOS.Modules.Inventory.Core.Features.Queries
{
    public class GetImportFilesQuery : IRequest<PaginatedResult<ImportFileDto>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string SearchString { get; private set; }

        public string[] OrderBy { get; private set; }
    }
}