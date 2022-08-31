using System.Collections.Generic;
using FluentPOS.Modules.Identity.Core.Entities;

namespace FluentPOS.Modules.Identity.Core.Dtos
{
    public class UserBranchModel
    {
        public List<UserBranch> UserBranchs { get; set; } = new();
    }
}