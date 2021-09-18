using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.IServices;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.DataAccess.IRepositories;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Implementations
{
    public class PayrollService : IPayrollService
    {
        #region Members

        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        #endregion Members

        #region Constructor

        public PayrollService(IRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        #endregion Constructor

        #region Events

        public async Task<bool> CalculatePayroll(PayrollDto dto)
        {
            try
            {
                switch (dto.EmployeeType)
                {
                    case (int)EmployeeType.Regular:
                        dto.GrossPay = Math.Round((dto.MonthlySalary - (dto.DaysAbsent * (dto.MonthlySalary / 22))), 2);
                        dto.NetPay = Math.Round((dto.GrossPay - (dto.MonthlySalary * (dto.TaxPercentage / 100))), 2);
                        break;
                    case (int)EmployeeType.Contractual:
                        dto.GrossPay = Math.Round((dto.DailyRate * dto.WorkedDays), 2);
                        dto.NetPay = dto.GrossPay;
                        break;
                    default:
                        throw new ArgumentException("Invalid Employee Type");
                }

                await SavePayroll(dto);
            }
            catch(Exception ex)
            {
                // logging...
                return false;
            }

            return true;
        }

        public async Task<bool> SavePayroll(PayrollDto dto)
        {
            try
            {
                var data = _mapper.Map<PayrollDto, Payroll>(dto);
                var createdById = 1;

                _repo.Create(data);
                await _repo.SaveAsync();
            }
            catch(Exception ex)
            {
                // logging...
                return false;
            }

            return true;
        }

        #endregion Events
    }
}
