    using CompanySystem.BusinessLogic.DTOS.Employees;

namespace CompanySystem.BusinessLogic.Services.Employees
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(string search);
        Task<EmployeeDetailsDto?> GetEmployeesByIdAsync(int id);
        Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto);
        Task<int> UpdateEmployeeAsync(UpdateEmployeeDto employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
