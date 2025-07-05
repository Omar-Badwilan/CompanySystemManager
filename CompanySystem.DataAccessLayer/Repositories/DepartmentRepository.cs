using CompanySystem.DataAccessLayer.Data.Contexts;


namespace CompanySystem.DataAccessLayer.Repositories
{
    class DepartmentRepository(ApplicationDbContext dbContext)
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        //CRUD Operations

        public Department? GetById(int id)
        {
            var department = _dbContext.Departments.Find(id);
            return department;
        }
    }
}
