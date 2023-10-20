using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Accounting.Core.Abstractions;
using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Modules.Accounting.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Accounting;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Dtos.Peoples;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Accounting.Infrastructure.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IOrgService _orgService;
        private readonly IEmployeeService _employeeService;
        private readonly IAccountingDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PayrollService> _localizer;

        public PayrollService(IAttendanceService attendanceService, IOrgService orgService, IEmployeeService employeeService, IAccountingDbContext context, IMapper mapper, IStringLocalizer<PayrollService> localizer)
        {
            _attendanceService = attendanceService;
            _orgService = orgService;
            _employeeService = employeeService;
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<bool> IsPayrollGenerated(DateTime dateTime)
        {
            return await _context.PayrollRequests.AnyAsync(x => x.Month == dateTime.Date.Month);
        }

        public async Task<bool> IsPayrollGenerated(Guid employeeId, DateTime dateTime)
        {
            dateTime = dateTime.AddHours(2);
            return await _context.Payrolls.AnyAsync(x => x.EmployeeId == employeeId && x.StartDate.Date.Month == dateTime.Month && x.StartDate.Date <= dateTime.Date && x.EndDate.Date >= dateTime.Date);
        }

        public async Task GeneratePayroll(Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest)
        {
            var payrollRequestEntry = await _context.PayrollRequests.Where(c => c.Id == payrollRequest.Id).AsNoTracking().FirstOrDefaultAsync();

            if (payrollRequestEntry == null)
            {
                throw new AccountingException(_localizer["Payroll Request Not Found!"], HttpStatusCode.NotFound);
            }

            if (payrollRequestEntry.Status == "Completed")
            {
                throw new AccountingException(_localizer["Payroll Request Already Completed!"], HttpStatusCode.Conflict);
            }

            // await _attendanceService.TiggerAutoAbsentJob(null);
            // return;
            try
            {
                DateTime startDate = DateTime.Now;
                DateTime endDate = DateTime.Now;

                switch (payrollRequest.PayPeriod)
                {
                    case Shared.DTOs.Enums.PayPeriod.Daily:
                        break;
                    case Shared.DTOs.Enums.PayPeriod.Weekly:
                        break;
                    case Shared.DTOs.Enums.PayPeriod.HalfMonth:
                        startDate = new DateTime(payrollRequest.StartDate.Year, payrollRequest.StartDate.Month, payrollRequest.StartDate.Day, 0, 1, 1);
                        endDate = new DateTime(payrollRequest.EndDate.Year, payrollRequest.EndDate.Month, payrollRequest.EndDate.Day, 23, 59, 59);
                        break;
                    case Shared.DTOs.Enums.PayPeriod.Monthly:
                        var firstDayOfMonth = new DateTime(startDate.Year, payrollRequest.Month, 1, 0, 1, 1);
                        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                        startDate = new DateTime(firstDayOfMonth.Year, firstDayOfMonth.Month, firstDayOfMonth.Day, 0, 1, 1);
                        endDate = new DateTime(lastDayOfMonth.Year, lastDayOfMonth.Month, lastDayOfMonth.Day, 23, 59, 59);
                        break;
                    case Shared.DTOs.Enums.PayPeriod.DateRange:
                        startDate = new DateTime(payrollRequest.StartDate.Year, payrollRequest.StartDate.Month, payrollRequest.StartDate.Day, 0, 1, 1);
                        endDate = new DateTime(payrollRequest.EndDate.Year, payrollRequest.EndDate.Month, payrollRequest.EndDate.Day, 23, 59, 59);
                        break;
                    default:
                        break;
                }

                if (startDate == endDate)
                {
                    return;
                }

                payrollRequest.StartDate = startDate;
                payrollRequest.EndDate = endDate;
                payrollRequest.LogList = new List<string>();
                var plocies = await _orgService.GetAllPoliciesAsync();
                var departments = await _orgService.GetAllDepartmentAsync();

                var plociesIds = plocies.Select(x => x.Id.Value).ToList();
                var employees = await _employeeService.GetEmployeeListByPayPeriodAsync(payrollRequest.PayPeriod);
                payrollRequest.EmployeeIds = new List<Guid>();
                foreach (var item in employees)
                {
                    bool result = await IsPayrollGenerated(item.Id.Value, startDate);
                    if (!result)
                    {
                        payrollRequest.EmployeeIds.Add(item.Id.Value);
                    }
                }

                List<Payroll> payrollList = new List<Payroll>();

                if (payrollRequest.EmployeeIds.Count > 0)
                {
                    // payrollRequest.EmployeeIds = employees.Select(x => x.Id.Value).ToList();
                    var attendances = await _attendanceService.GetEmployeeAttendance(payrollRequest.EmployeeIds, payrollRequest.StartDate, payrollRequest.EndDate);

                    // if (employees.Count > 0)
                    // {
                    //     await CalculatePayroll(payrollRequest, attendances);
                    // }
                    var results = attendances.GroupBy(p => p.EmployeeId, (key, g) => new { EmployeeId = key, Attendances = g.ToList() });

                    foreach (var item in results)
                    {
                        payrollRequest.EmployeeInfo = employees.FirstOrDefault(x => x.Id == item.EmployeeId);
                        if (payrollRequest.EmployeeInfo != null)
                        {
                            var salary = _context.Salaries.FirstOrDefault(x => x.EmployeeId == item.EmployeeId);
                            if (salary != null)
                            {
                                payrollRequest.EmployeeSalary = _mapper.Map<SalaryDto>(salary);
                            }
                            else
                            {
                                payrollRequest.LogList.Add($"{payrollRequest.EmployeeInfo.FullName}'s Salary Information Not Found!!");
                            }

                            var policyId = Guid.Empty;
                            if (payrollRequest.EmployeeInfo.PolicyId != default && payrollRequest.EmployeeInfo.PolicyId != Guid.Empty)
                            {
                                policyId = payrollRequest.EmployeeInfo.PolicyId;
                            }
                            else
                            {
                                var department = departments.FirstOrDefault(x => x.Id == payrollRequest.EmployeeInfo.DepartmentId);
                                if(department != null)
                                {
                                    policyId = department.PolicyId;
                                }
                            }

                            var policy = plocies.FirstOrDefault(x => x.Id == policyId);
                            if (policy != null)
                            {
                                payrollRequest.Policy = policy;
                            }
                            else
                            {
                                payrollRequest.LogList.Add($"{payrollRequest.EmployeeInfo.FullName}'sPolicy Information Not Found!!");
                            }

                            if (payrollRequest.Policy != null && payrollRequest.EmployeeSalary != null)
                            {
                                var payRoll = CalculateSalary(payrollRequest, item.Attendances);
                                payRoll.PaymentMode = payrollRequest.EmployeeInfo.PaymentMode;
                                if (payRoll.PaymentMode == Shared.DTOs.Enums.PaymentMode.Bank)
                                {
                                    payRoll.BankAccountNo = payrollRequest.EmployeeInfo.BankAccountNo;
                                    payRoll.BankAccountTitle = payrollRequest.EmployeeInfo.BankAccountTitle;
                                    payRoll.BankBranchCode = payrollRequest.EmployeeInfo.BankBranchCode;
                                    payRoll.BankName = payrollRequest.EmployeeInfo.BankName;
                                }

                                payrollList.Add(payRoll);
                            }
                        }
                        else
                        {
                            payrollRequest.LogList.Add($"{item.EmployeeId}'s Employee Information Not Found!!");
                        }
                    }
                }


                if (payrollList.Count > 0)
                {
                    _context.Payrolls.AddRange(payrollList);
                }
                payrollRequestEntry.Message = $"{payrollList.Count} Of {employees.Count} Employees Salaries Generated!";

                payrollRequestEntry.Logs = string.Join(",", payrollRequest.LogList);
                payrollRequestEntry.Status = "Completed";
                _context.PayrollRequests.Update(payrollRequestEntry);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                payrollRequest.LogList.Add(ex.Message);
                payrollRequestEntry.Logs = string.Join(",", payrollRequest.LogList);
                payrollRequestEntry.Status = "Failed";
                _context.PayrollRequests.Update(payrollRequestEntry);
                _context.SaveChanges();
                throw;
            }
        }

        private async Task CalculatePayroll(Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest, List<AttendanceDto> attendances)
        {
            var results = attendances.GroupBy(p => p.EmployeeId, (key, g) => new { EmployeeId = key, Attendances = g.ToList() });
            List<Payroll> payrollList = new List<Payroll>();
            foreach (var item in results)
            {
                payrollRequest.EmployeeInfo = await _employeeService.GetEmployeeDetailsAsync(item.EmployeeId);
                if (payrollRequest.EmployeeInfo != null)
                {
                    var policy = await _orgService.GetPolicyDetailsAsync(payrollRequest.EmployeeInfo.PolicyId);
                    if (policy != null)
                    {
                        payrollRequest.Policy = policy.Policy;
                        payrollList.Add(CalculateSalary(payrollRequest, item.Attendances));
                    }
                }
            }
        }

        private Payroll CalculateSalary(Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest, List<AttendanceDto> attendances)
        {
            Payroll payroll = new Payroll
            {
                StartDate = payrollRequest.StartDate,
                EndDate = payrollRequest.EndDate,
                AbsentDays = 0,
                AllowedOffDays = payrollRequest.Policy.AllowedOffDays,
                EarnedDays = 0,
                EmployeeSalary = payrollRequest.EmployeeSalary.BasicSalary,
                BranchId = payrollRequest.BranchId,
                CreatedAt = DateTime.Now,
                EmployeeId = payrollRequest.EmployeeInfo.Id.Value,
                leaves = 0,
                NetPay = 0,
                OrganizationId = payrollRequest.OrganizationId,
                PaymentMode = 0,
                PresentDays = 0,
                RequiredDays = 0,
                TotalDays = 0,
                TotalDeduction = 0,
                TotalEarned = 0,
                Transactions = new List<PayrollTransaction>(),
                TotalIncentive = 0,
                TotalOvertime = 0,
            };

            List<PayrollTransaction> transactions = new List<PayrollTransaction>();
            var presentList = attendances.Where(x => x.AttendanceType == Shared.DTOs.Enums.AttendanceType.Bio || x.AttendanceType == Shared.DTOs.Enums.AttendanceType.Manual || x.AttendanceType == Shared.DTOs.Enums.AttendanceType.System).ToList();
            payroll = CalculateAttendance(payroll, presentList, payrollRequest);

            if (!payrollRequest.IgnoreOvertime)
            {
                var overtimeList = attendances.Where(x => x.AttendanceType == Shared.DTOs.Enums.AttendanceType.OverTime).ToList();
                transactions.AddRange(CalculateOvertime(overtimeList, payrollRequest));
            }

            transactions.AddRange(GetPerks(payrollRequest, payroll));

            payroll.Transactions.AddRange(transactions);

            payroll.TotalEarned = payroll.Transactions.Where(x => x.TransactionType == Shared.DTOs.Enums.TransactionType.Earning).Sum(x => x.Earning);
            payroll.TotalIncentive = payroll.Transactions.Where(x => x.TransactionType == Shared.DTOs.Enums.TransactionType.Allowances).Sum(x => x.Earning);
            payroll.TotalDeduction = payroll.Transactions.Where(x => x.TransactionType == Shared.DTOs.Enums.TransactionType.Deduction).Sum(x => x.Deduction);
            payroll.TotalOvertime = payroll.Transactions.Where(x => x.TransactionType == Shared.DTOs.Enums.TransactionType.Earning && x.EntryType == Shared.DTOs.Enums.EntryType.Overtime).Sum(x => x.Earning);

            payroll.NetPay = payroll.TotalEarned + payroll.TotalIncentive - payroll.TotalDeduction;
            return payroll;
        }

        private List<PayrollTransaction> GetPerks(Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest, Payroll payroll)
        {
            DateTime payrollMonth = new DateTime(payrollRequest.StartDate.Year, payrollRequest.StartDate.Month, 01);
            List<PayrollTransaction> payrollTransactions = new List<PayrollTransaction>();
            var perks = _context.SalaryPerks.Where(x => x.EmployeeId == payroll.EmployeeId && (x.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives || x.Type == Shared.DTOs.Enums.SalaryPerksType.Deductions)).ToList();
            var globalPerks = _context.SalaryPerks.Where(x => x.IsGlobal == true && (x.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives || x.Type == Shared.DTOs.Enums.SalaryPerksType.Deductions)).ToList();
            var allPerks = globalPerks.Union(perks).ToList();
            foreach (var item in allPerks)
            {
                DateTime perkEffectiveMonth = new DateTime(item.EffecitveFrom.Year, item.EffecitveFrom.Month, 01);
                if (!IsPerksValid(payrollRequest, payroll, item))
                {
                    continue;
                }

                if (perkEffectiveMonth <= payrollMonth)
                {
                    if (!item.IsRecursion && perkEffectiveMonth == payrollMonth)
                    {
                        payrollTransactions.Add(new PayrollTransaction
                        {
                            Earning = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? item.Amount : 0.0,
                            Deduction = item.Type == Shared.DTOs.Enums.SalaryPerksType.Deductions ? item.Amount : 0.0,
                            DaysOrHours = 0,
                            TransactionName = item.Name,
                            TransactionType = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? Shared.DTOs.Enums.TransactionType.Allowances : Shared.DTOs.Enums.TransactionType.Deduction,
                            EntryType = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? Shared.DTOs.Enums.EntryType.Allowance : Shared.DTOs.Enums.EntryType.Deductions
                        });
                    }
                    else if (item.IsRecursion && item.IsRecursionUnLimited)
                    {
                        payrollTransactions.Add(new PayrollTransaction
                        {
                            Earning = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? item.Amount : 0.0,
                            Deduction = item.Type == Shared.DTOs.Enums.SalaryPerksType.Deductions ? item.Amount : 0.0,
                            DaysOrHours = 0,
                            TransactionName = item.Name,
                            TransactionType = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? Shared.DTOs.Enums.TransactionType.Allowances : Shared.DTOs.Enums.TransactionType.Deduction,
                            EntryType = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? Shared.DTOs.Enums.EntryType.Allowance : Shared.DTOs.Enums.EntryType.Deductions
                        });
                    }
                    else if (item.IsRecursion && !item.IsRecursionUnLimited)
                    {
                        DateTime perkEndingMonth = new DateTime(item.RecursionEndMonth.Value.Year, item.RecursionEndMonth.Value.Month, 01);
                        if (payrollMonth <= perkEndingMonth)
                        {
                            payrollTransactions.Add(new PayrollTransaction
                            {
                                Earning = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? item.Amount : 0.0,
                                Deduction = item.Type == Shared.DTOs.Enums.SalaryPerksType.Deductions ? item.Amount : 0.0,
                                DaysOrHours = 0,
                                TransactionName = item.Name,
                                TransactionType = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? Shared.DTOs.Enums.TransactionType.Allowances : Shared.DTOs.Enums.TransactionType.Deduction,
                                EntryType = item.Type == Shared.DTOs.Enums.SalaryPerksType.Incentives ? Shared.DTOs.Enums.EntryType.Allowance : Shared.DTOs.Enums.EntryType.Deductions
                            });
                        }
                    }
                }
            }

            return payrollTransactions;
        }

        private bool IsPerksValid(Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest, Payroll payroll, SalaryPerks salaryPerk)
        {
            if (salaryPerk.IsGlobal)
            {
                switch (salaryPerk.GlobalPerkType)
                {
                    case Shared.DTOs.Enums.GlobalSalaryPerksType.None:
                        break;
                    case Shared.DTOs.Enums.GlobalSalaryPerksType.EOBI:
                        return string.IsNullOrEmpty(payrollRequest.EmployeeInfo.EOBINo) == false;
                        break;
                    case Shared.DTOs.Enums.GlobalSalaryPerksType.FullMonthPresent:
                        return payroll.RequiredDays <= payroll.PresentDays;
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        private Payroll CalculateAttendance(Payroll payroll, List<AttendanceDto> attendances, Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest)
        {
            List<PayrollTransaction> transactions = new List<PayrollTransaction>();
            double totalEarnedHours = 0;
            int totalRequiredDays = 0;
            int totalMonthDays = 0;
            int totalAbsent = 0;
            int totalPresent = 0;
            int totalLeaves = 0;
            int totalHolidays = 0;
            int totalOffDays = 0;
            int totalDeductedHours = 0;
            double totalOvertimeHours = 0;

            if (!payrollRequest.IgnoreAttendance && attendances.Count == 0)
            {
                payrollRequest.LogList.Add("Attendance Information Not Found!!");
                return payroll;
            }

            foreach (var item in attendances.OrderBy(x => x.AttendanceDate))
            {
                totalMonthDays++;
                bool isWorkingDay = item.AttendanceStatus != Shared.DTOs.Enums.AttendanceStatus.Off || IsWorkingDay(item.AttendanceDate, payrollRequest.Policy);

                if (isWorkingDay)
                {
                    totalRequiredDays++;
                    switch (item.AttendanceStatus)
                    {
                        case Shared.DTOs.Enums.AttendanceStatus.Present:
                            totalPresent++;
                            totalEarnedHours += item.TotalEarnedHours;
                            if (item.DeductedHours > 0)
                            {
                                totalDeductedHours += item.DeductedHours;
                            }

                            if (item.OvertimeHours > 0)
                            {
                                totalOvertimeHours += item.OvertimeHours;
                            }

                            break;
                        case Shared.DTOs.Enums.AttendanceStatus.Absent:
                            totalAbsent++;
                            break;
                        case Shared.DTOs.Enums.AttendanceStatus.Leave:
                            totalLeaves++;
                            break;
                        case Shared.DTOs.Enums.AttendanceStatus.Holiday:
                            totalHolidays++;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    totalOffDays++;
                }
            }


            double salaryAmount = payrollRequest.EmployeeSalary.BasicSalary;
            string salaryTransactionName = $"Base Salary";
            if (payrollRequest.PayPeriod == Shared.DTOs.Enums.PayPeriod.HalfMonth)
            {
                salaryAmount = payrollRequest.EmployeeSalary.BasicSalary / 2;
                salaryTransactionName = $"Base Salary (15 Days)";
            }

            transactions.Add(new PayrollTransaction
            {
                TransactionType = Shared.DTOs.Enums.TransactionType.Earning,
                Earning = salaryAmount,
                TransactionName = salaryTransactionName,
                DaysOrHours = totalEarnedHours,
            });

            if (totalOvertimeHours > 0)
            {
                double totalOvertimeAmount = totalOvertimeHours * payrollRequest.EmployeeSalary.PerHourSalary;
                transactions.Add(new PayrollTransaction
                {
                    Earning = totalOvertimeAmount,
                    DaysOrHours = totalOvertimeHours,
                    TransactionName = $"Overtime ({totalOvertimeHours} Hrs)",
                    TransactionType = Shared.DTOs.Enums.TransactionType.Earning,
                    Deduction = 0,
                    EntryType = Shared.DTOs.Enums.EntryType.Overtime
                });
            }


            if (totalDeductedHours > 0 && !payrollRequest.IgnoreDeductionForLateComer)
            {
                transactions.Add(new PayrollTransaction
                {
                    TransactionType = Shared.DTOs.Enums.TransactionType.Deduction,
                    Deduction = totalDeductedHours * payrollRequest.EmployeeSalary.PerHourSalary,
                    TransactionName = $"Late Comer Panelity ({totalDeductedHours} Hrs.)",
                    DaysOrHours = totalDeductedHours,
                    EntryType = Shared.DTOs.Enums.EntryType.LateDeduction
                });
            }

            if (payrollRequest.Policy.AllowedOffDays < totalAbsent && !payrollRequest.IgnoreDeductionForAbsents)
            {
                int absents = totalAbsent;
                double deductionAmount = Math.Round(absents * payrollRequest.EmployeeSalary.PerDaySalary, 2);
                if (totalRequiredDays == totalAbsent)
                {
                    absents = totalAbsent + totalOffDays + totalHolidays;
                    deductionAmount = salaryAmount;
                }

                transactions.Add(new PayrollTransaction
                {
                    TransactionType = Shared.DTOs.Enums.TransactionType.Deduction,
                    Deduction = deductionAmount,
                    TransactionName = $"Absents ({absents} Days.)",
                    DaysOrHours = absents,
                    EntryType = Shared.DTOs.Enums.EntryType.AbsentDeduction
                });
            }

            payroll.RequiredDays = totalRequiredDays;
            payroll.TotalDeduction = totalDeductedHours;
            payroll.AbsentDays = totalAbsent;
            payroll.leaves = totalLeaves;
            payroll.PresentDays = totalPresent;
            payroll.EarnedDays = totalPresent;
            payroll.TotalDays = totalMonthDays;
            payroll.TotalOffDays = totalOffDays;
            payroll.TotalHoliDays = totalHolidays;

            payroll.Transactions.AddRange(transactions);
            return payroll;
        }

        private List<PayrollTransaction> CalculateOvertime(List<AttendanceDto> attendances, Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest)
        {
            List<PayrollTransaction> transactions = new List<PayrollTransaction>();
            double totalEarnedHours = 0;
            double totalEarnedAmount = 0;

            foreach (var item in attendances)
            {
                totalEarnedHours += item.TotalEarnedHours;
            }

            switch (payrollRequest.Policy.DailyOverTime)
            {
                case Shared.DTOs.Enums.OverTime.UnPaid:
                    totalEarnedAmount = totalEarnedHours * 0;
                    break;
                case Shared.DTOs.Enums.OverTime.EqualSalaryPerHour:
                    totalEarnedAmount = totalEarnedHours * payrollRequest.EmployeeSalary.PerHourSalary;
                    break;
                case Shared.DTOs.Enums.OverTime.SalaryPerHourMultiplyX:
                    totalEarnedAmount = totalEarnedHours * (payrollRequest.EmployeeSalary.PerHourSalary * payrollRequest.Policy.DailyOverTimeRate);
                    break;
                case Shared.DTOs.Enums.OverTime.FixedRate:
                    totalEarnedAmount = totalEarnedHours * payrollRequest.Policy.DailyOverTimeRate;
                    break;
                case Shared.DTOs.Enums.OverTime.EqualSalaryPerDay:
                    totalEarnedAmount = totalEarnedHours * payrollRequest.EmployeeSalary.PerDaySalary;
                    break;
                default:
                    totalEarnedAmount = 0;
                    break;
            }

            if (totalEarnedAmount > 0)
            {
                transactions.Add(new PayrollTransaction
                {
                    Earning = totalEarnedAmount,
                    DaysOrHours = totalEarnedHours,
                    TransactionName = $"Overtime ({totalEarnedHours} Hrs)",
                    TransactionType = Shared.DTOs.Enums.TransactionType.Earning,
                    Deduction = 0,
                    EntryType = Shared.DTOs.Enums.EntryType.Overtime
                });
            }

            return transactions;
        }

        private bool IsWorkingDay(DateTime date, PolicyDto policy)
        {
            bool isWorkingDay = false;
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    isWorkingDay = policy.IsSunday;
                    break;
                case DayOfWeek.Monday:
                    isWorkingDay = policy.IsMonday;
                    break;
                case DayOfWeek.Tuesday:
                    isWorkingDay = policy.IsTuesday;
                    break;
                case DayOfWeek.Wednesday:
                    isWorkingDay = policy.IsWednesday;
                    break;
                case DayOfWeek.Thursday:
                    isWorkingDay = policy.IsThursday;
                    break;
                case DayOfWeek.Friday:
                    isWorkingDay = policy.IsFriday;
                    break;
                case DayOfWeek.Saturday:
                    isWorkingDay = policy.IsSaturday;
                    break;
                default:
                    break;
            }

            return isWorkingDay;
        }

        public async Task InsertBasicSalary(Guid employeeId, decimal basicSalary)
        {
            bool exists = _context.Salaries.AsNoTracking().Any(x => x.EmployeeId == employeeId);
            if (!exists)
            {
                var salary = new Salary
                {
                    EmployeeId = employeeId,
                    Active = true,
                    BasicSalary = basicSalary,
                    CurrentSalary = basicSalary,
                    Deduction = 0,
                    Incentive = 0,
                    PayableSalary = basicSalary,
                    TotalDaysInMonth = 30,
                    PerDaySalary = basicSalary / 30, // TODO - get from policy
                    PerHourSalary = (basicSalary / 30) / 8 // TODO - get from policy
                };

                _context.Salaries.Add(salary);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SalaryIncrement(Guid employeeId, decimal increment)
        {
            var salary = _context.Salaries.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeId);
            if (salary != null)
            {
                salary.CurrentSalary += increment;
                salary.PayableSalary = salary.CurrentSalary + salary.Incentive - salary.Deduction;
                salary.PerDaySalary = salary.CurrentSalary / 30; // TODO - get from policy
                salary.PerHourSalary = (salary.CurrentSalary / 30) / 8; // TODO - get from policy

                _context.Salaries.Update(salary);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SalaryDecrement(Guid employeeId, decimal decrement)
        {
            var salary = _context.Salaries.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeId);
            if (salary != null)
            {
                salary.CurrentSalary -= decrement;
                salary.PayableSalary = salary.CurrentSalary + salary.Incentive - salary.Deduction;
                salary.PerDaySalary = salary.CurrentSalary / 30; // TODO - get from policy
                salary.PerHourSalary = (salary.CurrentSalary / 30) / 8; // TODO - get from policy

                _context.Salaries.Update(salary);
                await _context.SaveChangesAsync();
            }
        }
    }
}