using CompanySystem.BusinessLogic.Common.Services.Attachments;
using CompanySystem.BusinessLogic.DTOS.Employees;
using CompanySystem.DataAccessLayer.Models.Employees;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Employees;
using CompanySystem.DataAccessLayer.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace CompanySystem.BusinessLogic.Services.Employees
{
    public class EmployeeService(IUnitOfWork unitOfWork, IAttachmentService attachmentService) : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAttachmentService _attachmentService = attachmentService;

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search)
        {
            var employees = await _unitOfWork.EmployeeRepository
                .GetIQueryable()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
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
                }).ToListAsync();

            return employees;
        }
        public async Task<EmployeeDetailsDto?> GetEmployeesByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

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

        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
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

            if (employeeDto.Image is not null)
                employee.Image = await _attachmentService.UploadFileAsync(employeeDto.Image, "images");



            _unitOfWork.EmployeeRepository.Add(employee);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeDto.Id);
            if (employee is null)
                return 0;
            employee.Name = employeeDto.Name;
            employee.Age = employeeDto.Age;
            employee.Address = employeeDto.Address;
            employee.IsActive = employeeDto.IsActive;
            employee.Salary = employeeDto.Salary;
            employee.Email = employeeDto.Email;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.HiringDate = employeeDto.HiringDate;
            employee.Gender = employeeDto.Gender;
            employee.EmployeeType = employeeDto.EmployeeType;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.LastModifiedBy = 1;
            employee.LastModifiedOn = DateTime.UtcNow;

            //  Only update image if a new one was uploaded
            if (employeeDto.Image is not null)
            {
                employee.Image = await _attachmentService.UploadFileAsync(employeeDto.Image, "images");
            }
            _unitOfWork.EmployeeRepository.Update(employee);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;

            var employee = await employeeRepo.GetByIdAsync(id);

            if (employee is { })
                employeeRepo.Delete(employee);

            return await _unitOfWork.CompleteAsync() > 0;

        }
    }
}
