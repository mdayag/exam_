using AutoMapper;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.IServices;
using Sprout.Exam.DataAccess.IRepositories;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        #region Members

        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        #endregion Members

        #region Constructor

        public EmployeeService(IRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        #endregion Constructor

        #region Events

        public async Task<EmployeeDto> DeleteEmployee(int id)
        {
            var result = new EmployeeDto();

            try
            {
                var data = _mapper.Map<EmployeeDto, Employee>(await GetEmployeeById(id));
                data.IsDeleted = true;

                _repo.Update(data);
                await _repo.SaveAsync();

                result = _mapper.Map<Employee, EmployeeDto>(data);
            }
            catch (Exception ex)
            {
                // logging...
                return null;
            }

            return result;
        }

        public async Task<int> SaveEmployee(CreateEmployeeDto dto)
        {
            int id = 0;

            try
            {
                var data = _mapper.Map<CreateEmployeeDto, Employee>(dto);

                _repo.Create(data);
                await _repo.SaveAsync();

                // i may refactor this code
                id = _repo.GetFirst<Employee>(null, o => o.OrderByDescending(x => x.Id)).Result.Id;
            }
            catch(Exception ex)
            {
                // logging...
                return id;
            }

            return id;
        }

        public async Task<EmployeeDto> UpdateEmployee(EditEmployeeDto dto)
        {
            var result = new EmployeeDto();

            try
            {
                var data = await _repo.GetFirst<Employee>(o => o.Id == dto.Id && !o.IsDeleted, null, null);

                data = _mapper.Map<EditEmployeeDto, Employee>(dto);

                _repo.Update(data);
                await _repo.SaveAsync();

                result = _mapper.Map<Employee, EmployeeDto>(data);
            }
            catch (Exception ex)
            {
                // logging...
                return null;
            }

            return result;
        }

        public async Task<EmployeeDto> GetEmployeeById(int id)
        {
            try
            {
                var data = await _repo.GetFirst<Employee>(o => o.Id == id && !o.IsDeleted, null, null);
                return _mapper.Map<Employee, EmployeeDto>(data);
            }
            catch (Exception ex)
            {
                // logging...
                return null;
            }
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var employees = new List<EmployeeDto>();

            try
            {
                var data = await _repo.GetFiltered<Employee>(o => !o.IsDeleted, null, null);

                return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(data); ;
            }
            catch (Exception ex)
            {
                // logging...
                throw;
            }
        }

        #endregion Events
    }
}
