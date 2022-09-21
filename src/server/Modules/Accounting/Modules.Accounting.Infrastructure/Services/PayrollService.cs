using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Modules.Accounting.Core.Dtos;
using FluentPOS.Modules.Accounting.Core.Entities;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Interfaces.Services.Accounting;
using FluentPOS.Shared.Core.Interfaces.Services.Organization;
using FluentPOS.Shared.DTOs.Dtos.Organizations;
using FluentPOS.Shared.DTOs.Dtos.Peoples;

namespace FluentPOS.Modules.Accounting.Infrastructure.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IOrgService _orgService;
        private readonly IEmployeeService _employeeService;

        public PayrollService(IAttendanceService attendanceService, IOrgService orgService, IEmployeeService employeeService)
        {
            _attendanceService = attendanceService;
            _orgService = orgService;
            _employeeService = employeeService;
        }


        public async Task GeneratePayroll(Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest)
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

            var attendances = await _attendanceService.GetEmployeeAttendance(payrollRequest.EmployeeIds, payrollRequest.StartDate, payrollRequest.EndDate);

            if (attendances.Count > 0)
            {
                await CalculatePayroll(payrollRequest, attendances);
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
                CreateaAt = DateTime.Now,
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
                Transactions = new List<PayrollTransaction>()
            };

            List<PayrollTransaction> transactions = new List<PayrollTransaction>();
            var presentList = attendances.Where(x => x.AttendanceType == Shared.DTOs.Enums.AttendanceType.Bio || x.AttendanceType == Shared.DTOs.Enums.AttendanceType.Manual).ToList();
            payroll = CalculateAttendance(payroll, presentList, payrollRequest);

            if (!payrollRequest.IgnoreOvertime)
            {
                var overtimeList = attendances.Where(x => x.AttendanceType == Shared.DTOs.Enums.AttendanceType.OverTime).ToList();
                transactions.AddRange(CalculateOvertime(overtimeList, payrollRequest));
            }

            payroll.Transactions.AddRange(transactions);
            payroll.TotalEarned = payroll.Transactions.Where(x => x.TransactionType == Shared.DTOs.Enums.TransactionType.Earning).Sum(x => x.Earning);
            payroll.TotalDeduction = payroll.Transactions.Where(x => x.TransactionType == Shared.DTOs.Enums.TransactionType.Deduction).Sum(x => x.Deduction);
            payroll.TotalOvertime = payroll.Transactions.Where(x => x.TransactionType == Shared.DTOs.Enums.TransactionType.Earning && x.EntryType == Shared.DTOs.Enums.EntryType.Overtime).Sum(x => x.Earning);

            payroll.NetPay = payroll.TotalEarned + payroll.TotalIncentive - payroll.TotalDeduction;
            return payroll;
        }

        private Payroll CalculateAttendance(Payroll payroll, List<AttendanceDto> attendances, Shared.DTOs.Dtos.Accounting.PayrollRequestDto payrollRequest)
        {
            List<PayrollTransaction> transactions = new List<PayrollTransaction>();
            double totalEarnedHours = 0;
            int totalRequiredDays = 0;
            int totalAbsent = 0;
            int totalPresent = 0;
            int totalLeaves = 0;
            int totalHolidays = 0;
            int totalDeductedHours = 0;

            foreach (var item in attendances)
            {
                if (IsWorkingDay(item.AttendanceDate, payrollRequest.Policy))
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
                    totalHolidays++;
                }
            }

            if (payrollRequest.IgnoreAttendance)
            {
                transactions.Add(new PayrollTransaction
                {
                    TransactionType = Shared.DTOs.Enums.TransactionType.Earning,
                    Earning = payrollRequest.EmployeeSalary.BasicSalary,
                    TransactionName = $"Base Salary)",
                    DaysOrHours = totalEarnedHours,
                });
            }
            else if (totalEarnedHours > 0)
            {
                transactions.Add(new PayrollTransaction
                {
                    TransactionType = Shared.DTOs.Enums.TransactionType.Earning,
                    Earning = payrollRequest.EmployeeSalary.BasicSalary,
                    TransactionName = $"Base Salary)",
                    DaysOrHours = totalEarnedHours,
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
                int absents = totalAbsent - payrollRequest.Policy.AllowedOffDays;
                transactions.Add(new PayrollTransaction
                {
                    TransactionType = Shared.DTOs.Enums.TransactionType.Deduction,
                    Deduction = absents * payrollRequest.EmployeeSalary.PerDaySalary,
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
    }
}