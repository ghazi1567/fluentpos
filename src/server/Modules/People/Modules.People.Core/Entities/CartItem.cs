// --------------------------------------------------------------------------------------------------
// <copyright file="CartItem.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Modules.People.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public long CartId { get; set; }

        public virtual Cart Cart { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual ICollection<CartItemExtendedAttribute> ExtendedAttributes { get; set; }

        public CartItem()
        {
            ExtendedAttributes = new HashSet<CartItemExtendedAttribute>();
        }
    }
}