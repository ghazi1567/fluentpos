// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Modules.People.Core.Features.Employees.Commands;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Filters;
using FluentPOS.Shared.DTOs.People.Employees;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<RegisterEmployeeCommand, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Employee>, GetEmployeeByIdQuery>();
            CreateMap<GetEmployeeByIdResponse, Employee>().ReverseMap();
            CreateMap<GetEmployeesResponse, Employee>().ReverseMap();
            CreateMap<PaginatedFilter, GetEmployeesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}