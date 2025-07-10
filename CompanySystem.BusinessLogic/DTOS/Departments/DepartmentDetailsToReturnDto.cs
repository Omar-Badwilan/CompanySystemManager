using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BusinessLogic.DTOS.Departments
{
    public class DepartmentDetailsToReturnDto
    {
        public int Id { get; set; } 
        public int CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public int LastModifiedBy { get; set; } 
        public DateTime LastModifiedOn { get; set; }
        public string Name { get; set; } = null!; // Department Name 
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!; // Department Description
        public DateOnly CreationDate { get; set; }
    }
}
