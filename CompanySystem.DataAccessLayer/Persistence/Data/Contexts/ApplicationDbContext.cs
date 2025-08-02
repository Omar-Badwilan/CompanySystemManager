using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.DataAccessLayer.Models.Employees;
using System.Reflection;


namespace CompanySystem.DataAccessLayer.Persistence.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
