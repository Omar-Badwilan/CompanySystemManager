using CompanySystem.BusinessLogic.DTOS.Departments;
namespace CompanySystem.BusinessLogic.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsDto?> GetDepartmentsByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);
        Task<int> UpdateDepartmentAsync(UpdateDepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
