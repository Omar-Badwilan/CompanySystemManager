using CompanySystem.DataAccessLayer.Models.Employees;
using CompanySystem.DataAccessLayer.Persistence.Data.Contexts;
using CompanySystem.DataAccessLayer.Persistence.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories.Employees
{
    public class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employee>(dbContext), IEmployeeRepository
    {
        // The primary constructor is used to pass the ApplicationDbContext instance to the base class
    }
}
