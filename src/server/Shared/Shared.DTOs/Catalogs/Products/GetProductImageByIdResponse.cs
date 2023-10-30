using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public record GetProductImageByIdResponse(Guid Id, long? shopifyId, long? productId, int? Position, string src, string Filename, string Attachment, int? Height, int? Width, string Alt);
}
