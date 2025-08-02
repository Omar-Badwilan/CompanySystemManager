using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.DataAccessLayer.Persistence.Repositories._Generic;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Department> 
    {
        public IEnumerable<Department> GetSpecificDepartment();
    }
}
