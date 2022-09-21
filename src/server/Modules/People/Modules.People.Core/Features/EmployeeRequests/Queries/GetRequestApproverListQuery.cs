// --------------------------------------------------------------------------------------------------
// <copyright file="GetCustomersQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.People.Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetRequestApproverListQuery : IRequest<List<RequestApprovalDto>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }

        public Guid RequestId { get; set; }
    }
}