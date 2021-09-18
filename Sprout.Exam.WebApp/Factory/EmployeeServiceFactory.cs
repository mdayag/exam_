using AutoMapper;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Business.Implementations;
using Sprout.Exam.DataAccess.IRepositories;

namespace Sprout.Exam.WebApp.Factory
{
    public static class EmployeeServiceFactory
    {
        private static readonly IRepository _repository;
        private static readonly IMapper _mapper;

        //public static EmployeeService Create()
        //{
        //    var logger = new ILogger<EmployeeServiceFactory>();

        //    return new EmployeeService(logger);
        //}
    }
}
