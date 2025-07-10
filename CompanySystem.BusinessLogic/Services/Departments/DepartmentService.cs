using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.DataAccessLayer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.BusinessLogic.Services.Departments
{
    public class DepartmentService : IDepartmentService 
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllAsIQueryable().Select(department => new DepartmentToReturnDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate
            }).AsNoTracking().ToList();

            return departments;
        }

        public DepartmentDetailsToReturnDto? GetDepartmentsById(int id)
        {
            var department = _departmentRepository.GetById(id);

            if (department is { })
                return new DepartmentDetailsToReturnDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,
                    LastModifiedOn = department.LastModifiedOn,
                    LastModifiedBy = department.LastModifiedBy,
                };

            return null;
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = new Department()  
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                //CreatedOn = DateTime.UtcNow, it has defaultvaluesql on configurations
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow

            };

            return _departmentRepository.Add(department);
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow

            };
            return _departmentRepository.Update(department);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);

            if(department is { })
                return _departmentRepository.Delete(department) > 0;
            return false;
        }
    }
}
