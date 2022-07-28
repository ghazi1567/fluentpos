﻿using FluentPOS.Modules.Inventory.Core.Dtos;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Inventory.Core.Features.Reports
{
    public class StockOutReportQuery : SearchFilter, IRequest<Result<List<StockDto>>>
    {
    }

    public class StockOutReportQueryValidator : AbstractValidator<StockOutReportQuery>
    {
        public StockOutReportQueryValidator(IStringLocalizer<StockOutReportQueryValidator> localizer)
        {
            RuleFor(c => c.StartDate)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}