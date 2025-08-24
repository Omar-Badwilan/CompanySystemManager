using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.DataAccessLayer.Models.Employees;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;


namespace CompanySystem.DataAccessLayer.Persistence.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
