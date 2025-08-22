using CompanySystem.BusinessLogic.Common.Services.Attachments;
using CompanySystem.BusinessLogic.DTOS.Employees;
using CompanySystem.DataAccessLayer.Models.Employees;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Employees;
using CompanySystem.DataAccessLayer.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace CompanySystem.BusinessLogic.Services.Employees
{
    public class EmployeeService(IUnitOfWork unitOfWork ,IAttachmentService attachmentService) : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAttachmentService _attachmentService = attachmentService;

        public IEnumerable<EmployeeDto> GetEmployees(string search)
        {
            var employees = _unitOfWork.EmployeeRepository
                .GetIQueryable()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())) )
                .Include(E => E.Department)
                .Select(employee => new EmployeeDto()
                {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString(),
                Department = employee.Department != null ? employee.Department.Name : "No Department",
                }).ToList();

            return employees;
        }
        public EmployeeDetailsDto? GetEmployeesById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);

            if (employee is { })
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    DepartmentId = employee.DepartmentId,
                    Image = employee.Image,
                };

            return null;


        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {


            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            if(employeeDto.Image is not null)
                employee.Image = _attachmentService.Upload(employeeDto.Image, "images");



            _unitOfWork.EmployeeRepository.Add(employee);

            return _unitOfWork.Complete();
        }

        public int UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

               // ✅ Only update image if a new one was uploaded
    if (employeeDto.Image is not null)
    {
        employee.Image = _attachmentService.Upload(employeeDto.Image, "images");
    }
            _unitOfWork.EmployeeRepository.Update(employee);

            return _unitOfWork.Complete();
        }

        public bool DeleteEmployee(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;

            var employee = employeeRepo.GetById(id);

            if (employee is { })
                 employeeRepo.Delete(employee);

            return _unitOfWork.Complete() > 0;

        }
    }
}
