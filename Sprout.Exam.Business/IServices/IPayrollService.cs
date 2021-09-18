using Sprout.Exam.Business.DataTransferObjects;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.IServices
{
    public interface IPayrollService
    {
        Task<bool> SavePayroll(PayrollDto dto);

        Task<bool> CalculatePayroll(PayrollDto payrollDto);
    }
}
