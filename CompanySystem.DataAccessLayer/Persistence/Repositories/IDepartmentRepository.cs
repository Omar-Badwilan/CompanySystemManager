using CompanySystem.DataAccessLayer.Models.Departments;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withAsNoTracking = true);
        IQueryable<Department> GetAllAsIQueryable();

        Department? GetById(int id);
         
        int Add(Department department);

        int Update(Department department);

        int Delete(Department department);
    }
}
