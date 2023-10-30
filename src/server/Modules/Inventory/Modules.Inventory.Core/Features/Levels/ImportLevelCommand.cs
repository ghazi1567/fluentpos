using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Upload;
using MediatR;
using System;

namespace FluentPOS.Modules.Inventory.Core.Features.Levels
{
    public class ImportLevelCommand : IRequest<Result<Guid>>
    {
        public UploadRequest UploadRequest { get; set; }

        public ImportFileDto ImportFile { get; set; }
    }
}