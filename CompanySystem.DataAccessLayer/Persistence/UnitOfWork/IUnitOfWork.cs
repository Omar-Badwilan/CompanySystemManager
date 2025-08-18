
using CompanySystem.DataAccessLayer.Persistence.Repositories.Departments;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Employees;

namespace CompanySystem.DataAccessLayer.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get;}
        public IDepartmentRepository DepartmentRepository { get;}

        int Complete();
    }
}
