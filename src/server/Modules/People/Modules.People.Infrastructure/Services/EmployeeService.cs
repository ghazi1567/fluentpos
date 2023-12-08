using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.DTOs.Enums;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(IPeopleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeListAsync()
        {
            var employee = await _context.Employees.Where(x => x.Active == true).ToListAsync();
            return _mapper.Map<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>>(employee);
        }

        public async Task<int> GetEmployeeCountAsync(bool isActiveOnly)
        {
            var queryable = _context.Employees.AsQueryable();

            if (isActiveOnly)
            {
                queryable = queryable.Where(x => x.Active == true);
            }

            return await queryable.CountAsync();
        }

        public async Task<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto> GetEmployeeDetailsAsync(long Id)
        {
            var employee = await _context.Employees.Where(c => c.Id == Id).FirstOrDefaultAsync(default(CancellationToken));
            if (employee == null)
            {
                return default;
            }

            return _mapper.Map<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>(employee);
        }

        public async Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeDetailsAsync(List<long> Ids)
        {
            var employee = await _context.Employees.Where(c => Ids.Contains(c.Id)).ToListAsync(default(CancellationToken));
            if (employee == null)
            {
                return default;
            }

            return _mapper.Map<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>>(employee);
        }

        public async Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeListByPolicyAsync(List<long> Ids)
        {
            var employee = await _context.Employees.Where(c => c.Active == true && Ids.Contains(c.PolicyId)).ToListAsync(default(CancellationToken));
            if (employee == null)
            {
                return default;
            }

            return _mapper.Map<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>>(employee);
        }

        public async Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetEmployeeListByPayPeriodAsync(PayPeriod payPeriod)
        {
            var employee = await _context.Employees.Where(c => c.Active == true && c.PayPeriod == payPeriod).ToListAsync(default(CancellationToken));
            if (employee == null)
            {
                return default;
            }

            return _mapper.Map<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>>(employee);
        }

        public async Task<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>> GetMyReporterEmployeeListAsync(long id, bool includeMe = false)
        {
            var employee = await _context.Employees.Where(x => x.ReportingTo == id).ToListAsync();
            if (includeMe)
            {
                var me = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (me != null)
                {
                    employee.Add(me);
                }
            }

            return _mapper.Map<List<Shared.DTOs.Dtos.Peoples.BaseEmployeeDto>>(employee);
        }
    }
}