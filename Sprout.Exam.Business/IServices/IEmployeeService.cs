using Sprout.Exam.Business.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.IServices
{
    public interface IEmployeeService
    {
        Task<int> SaveEmployee(CreateEmployeeDto dto);
        Task<EmployeeDto> UpdateEmployee(EditEmployeeDto dto);
        Task<EmployeeDto> DeleteEmployee (int id);
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<IEnumerable<EmployeeDto>> GetEmployees();
    }
}
