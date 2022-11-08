// --------------------------------------------------------------------------------------------------
// <copyright file="ProductProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Modules.People.Core.Features.Salaries.Commands;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Modules.Accounting.Core.Mappings
{
    public class AccountingProfile : Profile
    {
        public AccountingProfile()
        {
            CreateMap<PaginatedResult<SalaryDto>, PaginatedResult<Salary>>().ReverseMap();
            CreateMap<SalaryDto, Salary>().ReverseMap();
            CreateMap<RegisterSalaryCommand, Salary>().ReverseMap();
            CreateMap<UpdateSalaryCommand, Salary>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Salary>, GetSalaryByIdQuery>();
            CreateMap<GetProductByIdResponse, Salary>().ReverseMap();
            CreateMap<PaginatedFilter, GetSalaryQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));



            CreateMap<PaginatedResult<SalaryPerksDto>, PaginatedResult<SalaryPerks>>().ReverseMap();
            CreateMap<SalaryPerksDto, SalaryPerks>().ReverseMap();
            CreateMap<RegisterSalaryPerksCommand, SalaryPerks>().ReverseMap();
            CreateMap<UpdateSalaryPerksCommand, SalaryPerks>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, SalaryPerks>, GetSalaryByIdQuery>();
            CreateMap<GetProductByIdResponse, SalaryPerks>().ReverseMap();
            CreateMap<PaginatedFilter, GetSalaryQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));


            CreateMap<PaginatedResult<PayrollRequestDto>, PaginatedResult<PayrollRequest>>().ReverseMap();
            CreateMap<PayrollRequestDto, PayrollRequest>().ReverseMap();
            CreateMap<RegisterSalaryPerksCommand, PayrollRequest>().ReverseMap();
            CreateMap<UpdateSalaryPerksCommand, PayrollRequest>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, PayrollRequest>, GetPayrollRequestByIdQuery>();
            CreateMap<GetPayrollRequestByIdQuery, PayrollRequest>().ReverseMap();
            CreateMap<PaginatedFilter, GetPayrollRequestQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));


            CreateMap<PaginatedResult<Payroll>, PaginatedResult<PayrollDto>>().ReverseMap();
            CreateMap<PayrollDto, Payroll>().ReverseMap();
            CreateMap<PayrollTransactionDto, PayrollTransaction>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Payroll>, GetPayslipRequestByIdQuery>();
            CreateMap<GetPayslipRequestByIdQuery, Payroll>().ReverseMap();
            CreateMap<PaginatedFilter, GetPayslipRequestQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}