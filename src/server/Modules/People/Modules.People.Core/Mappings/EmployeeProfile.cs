// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeProfile.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Modules.People.Core.Features.Employees.Commands;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Filters;
using FluentPOS.Shared.DTOs.People.EmployeeRequests;
using FluentPOS.Shared.DTOs.People.Employees;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Shared.DTOs.Dtos.Peoples.EmployeeDto, Employee>().ReverseMap();
            CreateMap<RegisterEmployeeCommand, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Employee>, GetEmployeeByIdQuery>();
            CreateMap<GetEmployeeByIdResponse, Employee>().ReverseMap();
            CreateMap<GetEmployeesResponse, Employee>().ReverseMap();
            CreateMap<PaginatedFilter, GetEmployeesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));


            CreateMap<PaginatedResult<EmployeeRequestDto>, PaginatedResult<EmployeeRequest>>().ReverseMap();
            CreateMap<EmployeeRequestDto, EmployeeRequest>().ReverseMap();
            CreateMap<RegisterEmployeeRequestCommand, EmployeeRequest>().ReverseMap();
            CreateMap<UpdateEmployeeRequestCommand, EmployeeRequest>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, EmployeeRequest>, GetEmployeeRequestByIdQuery>();
            CreateMap<GetEmployeeRequestByIdResponse, EmployeeRequest>().ReverseMap();
            CreateMap<GetEmployeeRequestsResponse, EmployeeRequest>().ReverseMap();
            CreateMap<PaginatedFilter, GetEmployeeRequestsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<PaginatedFilter, GetMyQueueQuery>()
               .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<PaginatedFilter, GetRequestApproverListQuery>()
              .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));

            CreateMap<PaginatedResult<AttendanceDto>, PaginatedResult<Attendance>>().ReverseMap();
            CreateMap<PaginatedResult<Shared.DTOs.Dtos.Peoples.AttendanceDto>, PaginatedResult<Attendance>>().ReverseMap();
            CreateMap<AttendanceDto, Attendance>().ReverseMap();
            CreateMap<Shared.DTOs.Dtos.Peoples.AttendanceDto, Attendance>().ReverseMap();
            CreateMap<PaginatedFilter, GetAttendanceQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<PaginatedFilter, GetIndividualReportQuery>();
            CreateMap<BioAttendanceLogDto, BioAttendanceLog>().ReverseMap();
            CreateMap<PaginatedResult<BioAttendanceLogDto>, PaginatedResult<BioAttendanceLog>>().ReverseMap();
        }
    }
}