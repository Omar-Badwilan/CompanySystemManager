using CompanySystem.BusinessLogic.DTOS.Departments;
namespace CompanySystem.BusinessLogic.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentsById(int id);
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
    }
}
