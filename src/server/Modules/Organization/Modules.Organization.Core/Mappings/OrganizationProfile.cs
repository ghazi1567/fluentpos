using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Features;
using FluentPOS.Modules.Catalog.Core.Features.Branchs.Queries;
using FluentPOS.Modules.Organization.Core.Dtos;
using FluentPOS.Modules.Organization.Core.Entities;
using FluentPOS.Modules.Organization.Core.Features;
using FluentPOS.Modules.Organization.Core.Features.Branchs.Commands;
using FluentPOS.Modules.Organization.Core.Features.Organizations.Commands;
using FluentPOS.Modules.Organizations.Core.Features;
using FluentPOS.Modules.Organizations.Core.Features.Organizations.Queries;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs;
using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Filters;
using FluentPOS.Shared.DTOs.Organizations.Branchs;
using FluentPOS.Shared.DTOs.Organizations.Departments;
using FluentPOS.Shared.DTOs.Organizations.Designations;
using FluentPOS.Shared.DTOs.Organizations.Policies;
using System;

namespace FluentPOS.Modules.Organization.Core.Mappings
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<RegisterOrganizationCommand, Organisation>().ReverseMap();
            CreateMap<UpdateOrganizationCommand, Organisation>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Organisation>, GetOrganizationByIdQuery>();
            CreateMap<GetOrganizationByIdResponse, Organisation>().ReverseMap();
            CreateMap<GetOrganizationResponse, Organisation>().ReverseMap();
            CreateMap<PaginatedOrganizationFilter, GetOrganizationsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));

            CreateMap<RegisterBranchCommand, Branch>().ReverseMap();
            CreateMap<UpdateBranchCommand, Branch>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Branch>, GetBranchByIdQuery>();
            CreateMap<GetBranchByIdResponse, Branch>().ReverseMap();
            CreateMap<GetBranchResponse, Branch>().ReverseMap();
            CreateMap<PaginatedBrachFilter, GetBranchsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));

            CreateMap< Shared.DTOs.Dtos.Organizations.DepartmentDto, Department>().ReverseMap();
            CreateMap<RegisterDepartmentCommand, Department>().ReverseMap();
            CreateMap<UpdateDepartmentCommand, Department>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Department>, GetDepartmentByIdQuery>();
            CreateMap<GetDepartmentByIdResponse, Department>().ReverseMap();
            CreateMap<GetDepartmentResponse, Department>().ReverseMap();
            CreateMap<GetDepartmentResponse, Dtos.DepartmentDto>().ReverseMap();
            CreateMap<PaginatedFilter, GetDepartmentsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<PaginatedResult<Dtos.DepartmentDto>, PaginatedResult<GetDepartmentResponse>>().ReverseMap();

            CreateMap<RegisterDesignationCommand, Designation>().ReverseMap();
            CreateMap<UpdateDesignationCommand, Designation>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Designation>, GetDesignationByIdQuery>();
            CreateMap<GetDesignationByIdResponse, Designation>().ReverseMap();
            CreateMap<GetDesignationResponse, Designation>().ReverseMap();
            CreateMap<PaginatedFilter, GetDesignationsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));

            CreateMap<RegisterPolicyCommand, Policy>().ReverseMap();
            CreateMap<UpdatePolicyCommand, Policy>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Policy>, GetPolicyByIdQuery>();
            CreateMap<GetPolicyByIdResponse, Policy>().ReverseMap();
            CreateMap<GetPolicyResponse, Policy>().ReverseMap();
            CreateMap<PaginatedFilter, GetPoliciesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));

            CreateMap<Shared.DTOs.Dtos.Organizations.PolicyDto, Policy>().ReverseMap();

            CreateMap<JobDto, Job>().ReverseMap();
            CreateMap<PaginatedResult<JobDto>, PaginatedResult<Job>>().ReverseMap();
            CreateMap<RegisterJobCommand, Job>().ReverseMap();
            CreateMap<UpdateJobCommand, Job>().ReverseMap();
            CreateMap<PaginatedFilter, GetJobsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}