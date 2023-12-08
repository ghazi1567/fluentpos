﻿// --------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Utilities;

namespace FluentPOS.Shared.Core.Domain
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        public long AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().GetGenericTypeName();
        }
    }
}