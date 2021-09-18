using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.DataAccess.Models;

namespace Sprout.Exam.WebApp.Mapper
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig()
        {
            //CreateMap<Payroll, PayrollDto>()
            //    .ForMember(tl => tl.EmployeeType, opts => opts.MapFrom(source => source.TypeId));

            //CreateMap<PayrollDto, Payroll>()
            //    .ForMember(tl => tl.TypeId, opts => opts.MapFrom(source => source.EmployeeType));

            CreateMap<Employee, EmployeeDto>()
                .ForMember(tl => tl.TypeId, opts => opts.MapFrom(source => source.EmployeeTypeId));

            CreateMap<EmployeeDto, Employee>()
                .ForMember(tl => tl.EmployeeTypeId, opts => opts.MapFrom(source => source.TypeId));

            CreateMap<Employee, EditEmployeeDto>()
                .ForMember(tl => tl.TypeId, opts => opts.MapFrom(source => source.EmployeeTypeId));

            CreateMap<EditEmployeeDto, Employee>()
                .ForMember(tl => tl.EmployeeTypeId, opts => opts.MapFrom(source => source.TypeId));

            CreateMap<Employee, CreateEmployeeDto>()
                .ForMember(tl => tl.TypeId, opts => opts.MapFrom(source => source.EmployeeTypeId));

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(tl => tl.EmployeeTypeId, opts => opts.MapFrom(source => source.TypeId));
        }
    }
}
