using CompanySystem.DataAccessLayer.Persistence.Data.Contexts;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Departments;
using CompanySystem.DataAccessLayer.Persistence.Repositories.Employees;

namespace CompanySystem.DataAccessLayer.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository => new EmployeeRepository(_dbContext);

        public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

    }
}
