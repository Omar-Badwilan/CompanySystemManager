    using CompanySystem.BusinessLogic.DTOS.Employees;

namespace CompanySystem.BusinessLogic.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(string search);
        EmployeeDetailsDto? GetEmployeesById(int id);
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdateEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
