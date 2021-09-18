using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.IServices;
using System;

namespace Sprout.Exam.WebApp.Controllers
{
    [Produces("application/json")]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Gets list of employees
        /// </summary>
        /// <returns>Enumerable<EmployeeDto></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employeeService.GetEmployees();
            return Ok(result);
        }

        /// <summary>
        /// Get employee details
        /// </summary>
        /// <returns>EmployeeDto</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeDto), 200)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            return Ok(result);
        }

        /// <summary>
        /// Updates employee information
        /// </summary>
        /// <returns>EmployeeDto</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditEmployeeDto), 200)]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var item = await _employeeService.UpdateEmployee(input);
            if (item == null) return NotFound();
            item.FullName = input.FullName;
            item.Tin = input.Tin;
            item.Birthdate = input.Birthdate.ToString("yyyy-MM-dd");
            item.TypeId = input.TypeId;
            return Ok(item);
        }

        /// <summary>
        /// Stores employee information
        /// </summary>
        /// <returns>int</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), 201)]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var id = await _employeeService.SaveEmployee(input);

            return Created($"/api/employees/{id}", id);
        }


        /// <summary>
        /// Soft deletes employee information
        /// </summary>
        /// <returns>int</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (result == null) return NotFound();

            return Ok(id);
        }



        /// <summary>
        /// Calculates employee's payroll
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns>decimal</returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, decimal absentDays, decimal workedDays)
        {
            var result = await _employeeService.GetEmployeeById(id);

            if (result == null) return NotFound();
            var type = (EmployeeType) result.TypeId;

            return type switch
            {
                EmployeeType.Regular =>
                    Ok(CalculatePayroll(type, absentDays, workedDays)),
                EmployeeType.Contractual =>
                    Ok(CalculatePayroll(type, absentDays, workedDays)),
                _ => NotFound("Employee Type not found")
            };
        }

        private decimal CalculatePayroll(EmployeeType type, decimal absentDays, decimal workedDays)
        {
            decimal netPay = 0;

            try
            {
                decimal taxPercentage = 12; // static value for the mean time
                decimal monthlySalary = 20000; // static value for the mean time
                decimal dailyRate = 500; // static value for the mean time

                decimal grossPay = 0;

                switch (type)
                {
                    case EmployeeType.Regular:
                        grossPay = Math.Round((monthlySalary - (absentDays * (monthlySalary / 22))), 2);
                        netPay = Math.Round((grossPay - (monthlySalary * (taxPercentage / 100))), 2);
                        break;
                    case EmployeeType.Contractual:
                        grossPay = Math.Round((dailyRate * workedDays), 2);
                        netPay = grossPay;
                        break;
                }
            }
            catch (Exception ex)
            {
                // error log...
            }

            return netPay;
        }
    }
}
