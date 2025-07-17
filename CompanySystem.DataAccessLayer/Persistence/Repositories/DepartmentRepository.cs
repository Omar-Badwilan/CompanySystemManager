using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.DataAccessLayer.Persistence.Data.Contexts;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;


        //CRUD Operations
        public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        {
            if(withAsNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();

            return _dbContext.Departments.ToList();
        }

        public IQueryable<Department> GetAllAsIQueryable()
        {
            return _dbContext.Departments;
        }
        public Department? GetById(int id)
        {
            return _dbContext.Departments.Find(id);
        }
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }



    }
}
