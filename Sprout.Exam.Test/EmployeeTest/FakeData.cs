using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprout.Exam.Test.EmployeeTest
{
    public class FakeData
    {
        public static IEnumerable<Employee> GetSampleEmployees(bool hasData)
        {
            if (hasData == false)
                return new List<Employee>().AsEnumerable();

            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Birthdate = DateTime.Parse("1993-03-05 11:45:33.737"),
                    FullName = "Jane Doe",
                    Tin = "123215413",
                    EmployeeTypeId = 1
                },
                new Employee
                {
                    Id = 2,
                    Birthdate = DateTime.Parse("1993-05-28 11:45:33.737"),
                    FullName = "John Doe",
                    Tin = "957125412",
                    EmployeeTypeId = 2
                }
            };
        }

        public static IEnumerable<EmployeeDto> GetSampleEmployeesDto(bool hasData)
        {
            if (hasData == false)
                return new List<EmployeeDto>().AsEnumerable();

            return new List<EmployeeDto>
            {
                new EmployeeDto
                {
                    Birthdate = "1993-03-25",
                    FullName = "Jane Doe",
                    Id = 1,
                    Tin = "123215413",
                    TypeId = 1
                },
                new EmployeeDto
                {
                    Birthdate = "1993-05-28",
                    FullName = "John Doe",
                    Id = 2,
                    Tin = "957125412",
                    TypeId = 2
                }
            };
        }

        public static Employee GetSampleEmployee(bool hasData)
        {
            if (hasData == false)
                return new Employee();

            return new Employee
            {
                Id = 1,
                Birthdate = DateTime.Parse("1993-03-05 11:45:33.737"),
                FullName = "Jane Doe",
                Tin = "123215413",
                EmployeeTypeId = 1
            };
        }

        public static CreateEmployeeDto GetSampleCreateEmployeeDto(bool hasData)
        {
            if (hasData == false)
                return new CreateEmployeeDto();

            return new CreateEmployeeDto
            {
                Birthdate = DateTime.Parse("1993-03-05 11:45:33.737"),
                FullName = "Jane Doe",
                Tin = "123215413",
                TypeId = 1
            };
        }

        public static EmployeeDto GetSampleEmployeeDtoById(bool hasData)
        {
            if (hasData == false)
                return new EmployeeDto();

            return new EmployeeDto
            {
                Id = 1,
                Birthdate = "1993-03-25",
                FullName = "Jane Doe",
                Tin = "123215413",
                TypeId = 1
            };
        }
    }
}
