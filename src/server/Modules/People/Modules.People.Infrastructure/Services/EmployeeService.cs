using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Dtos;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.DTOs.People.EmployeeRequests;
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

        public async Task<Shared.DTOs.Dtos.Peoples.EmployeeDto> GetEmployeeDetailsAsync(Guid Id)
        {
            var employee = await _context.Employees.Where(c => c.Id == Id).FirstOrDefaultAsync(default(CancellationToken));
            if (employee == null)
            {
                return default;
            }

            return _mapper.Map<Shared.DTOs.Dtos.Peoples.EmployeeDto>(employee);
        }
    }
}