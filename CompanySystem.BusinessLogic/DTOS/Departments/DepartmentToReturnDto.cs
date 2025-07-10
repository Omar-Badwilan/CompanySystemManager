using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BusinessLogic.DTOS.Departments
{
    public class DepartmentToReturnDto
    {
        public int Id { get; set; } // Pk
        public string Name { get; set; } = null!; // Department Name 
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;// Department Description

        public DateOnly CreationDate { get; set; }
    }
}
