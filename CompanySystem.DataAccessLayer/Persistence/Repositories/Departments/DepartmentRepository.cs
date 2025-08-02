using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.DataAccessLayer.Persistence.Data.Contexts;
using CompanySystem.DataAccessLayer.Persistence.Repositories._Generic;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext) //ASK CLR for Object from app db context , in employees it is primary constructor
        {
        }

        public IEnumerable<Department> GetSpecificDepartment()
        {
            //_dbContext  because of private protected access modifier in GenericRepository
            throw new NotImplementedException();
        }
    }
}
