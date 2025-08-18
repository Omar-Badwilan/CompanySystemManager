using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Departments;
using CompanySystem.DataAccessLayer.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.BusinessLogic.Services.Departments
{
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService 
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository
                .GetIQueryable()
                .Where(d=>!d.IsDeleted)
                .Select(department => new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                CreationDate = department.CreationDate 
            }).AsNoTracking().ToList();

            return departments;
        }

        public DepartmentDetailsDto? GetDepartmentsById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);

            if (department is { })
                return new DepartmentDetailsDto
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

             _unitOfWork.DepartmentRepository.Add(department);

            return _unitOfWork.Complete();
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow

            };
            _unitOfWork.DepartmentRepository.Update(department);

            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int id)
        {

            var departmentRepo =_unitOfWork.DepartmentRepository;
            var department = departmentRepo.GetById(id);

            if(department is { })
                departmentRepo.Delete(department);

            return _unitOfWork.Complete() > 0;
        }
    }
}
