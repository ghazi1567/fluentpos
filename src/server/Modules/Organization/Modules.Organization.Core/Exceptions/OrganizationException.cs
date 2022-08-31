// --------------------------------------------------------------------------------------------------
// <copyright file="SalesException.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Net;
using FluentPOS.Shared.Core.Exceptions;

namespace FluentPOS.Modules.Organization.Core.Exceptions
{
    public class OrganizationException : CustomException
    {
        public OrganizationException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message, statusCode: statusCode)
        {
        }
    }
}