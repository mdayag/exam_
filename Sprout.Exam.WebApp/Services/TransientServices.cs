using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.Business.Implementations;
using Sprout.Exam.Business.IServices;
using Sprout.Exam.DataAccess.IRepositories;
using Sprout.Exam.DataAccess.Repositories;
using Sprout.Exam.WebApp.DataAccess;

namespace Sprout.Exam.WebApp.Services
{
    public static class TransientServices
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository<ApplicationDbContext>>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
