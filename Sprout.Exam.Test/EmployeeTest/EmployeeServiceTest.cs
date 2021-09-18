using AutoMapper;
using Moq;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Implementations;
using Sprout.Exam.Business.IServices;
using Sprout.Exam.DataAccess.IRepositories;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Test.EmployeeTest
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IRepository> _repo;
        private readonly Mock<IMapper> _mapper;
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IEmployeeService> _employeeServiceMoq;
        
        public EmployeeServiceTest()
        {
            _repo = new Mock<IRepository>();
            _mapper = new Mock<IMapper>();
            _employeeService = new EmployeeService(_repo.Object, _mapper.Object);
            _employeeServiceMoq = new Mock<IEmployeeService>();
        }

        [Fact]
        public void When_GetEmployeeData_Expect_HasData()
        {
            GetEmployeesSetUp(true);

            var employees = _employeeService.GetEmployees();
            var expected = employees.Result;

            Assert.NotEmpty(expected);
        }

        [Fact]
        public void When_RecordEmpty_Expect_HasNoData()
        {
            GetEmployeesSetUp(false);

            var employees = _employeeService.GetEmployees();
            var expected = employees.Result;

            Assert.Empty(expected);
        }

        [Fact]
        public void When_IdIsDigitAndNotZeroOrLess_Expect_EqualId()
        {
            int id = 1;
            GetEmployeeByIdSetUp(true);

            var employees = _employeeService.GetEmployeeById(id);
            var actual = employees.Result;
            var expected = FakeData.GetSampleEmployeeDtoById(true);

            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public void When_IdIsLessThanZero_Expect_ThrowException()
        {
            int id = -1;

            var actual = Assert.ThrowsAsync<ArgumentException>(() => _employeeService.GetEmployeeById(id));

            var expected = $"Invalid parameter id: {id}";

            Assert.Equal(expected, actual.Result.Message);
        }

        [Fact]
        public void When_Save_Expect_MethodCalled()
        {
            var employeeData = FakeData.GetSampleCreateEmployeeDto(true);

            _employeeServiceMoq.Object.SaveEmployee(employeeData);

            _employeeServiceMoq.Verify(x => x.SaveEmployee(employeeData), Times.Once);
        }

        [Fact]
        public void When_DeleteIdIsGiven_Expect_RecordIsFound()
        {
            var id = 1;

            DeleteEmployeeSetup(true);

            var employee = _employeeService.DeleteEmployee(id);

            _repo.Verify(x => x.SaveAsync(), Times.Once);

            Assert.NotNull(employee.Result);
        }

        private void GetEmployeesSetUp(bool hasData)
        {
            _repo.Setup(x => x.GetAllAsync<Employee>(null, null, null, null, null))
                .Returns(Task.FromResult(FakeData.GetSampleEmployees(hasData)));

            _mapper.Setup(x => x.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(It.IsAny<IEnumerable<Employee>>()))
                .Returns(FakeData.GetSampleEmployeesDto(hasData));
        }

        private void GetEmployeeByIdSetUp(bool hasData)
        {
            _repo.Setup(x => x.GetByIdAsync<Employee>(It.IsAny<int>()))
               .Returns(Task.FromResult(FakeData.GetSampleEmployee(hasData)));

            _mapper.Setup(x => x.Map<Employee, EmployeeDto>(It.IsAny<Employee>()))
                .Returns(FakeData.GetSampleEmployeeDtoById(hasData));
        }

        private void DeleteEmployeeSetup(bool hasData)
        {
            _repo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<Employee, bool>>>(), null, null, null))
               .Returns(Task.FromResult(FakeData.GetSampleEmployee(hasData)));

            _repo.Setup(x => x.Update(FakeData.GetSampleEmployee(hasData)));
        }
    }
}
