using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.BusinessLogic.DTOS.Departments
{
    public class DepartmentDetailsDto
    {
        public int Id { get; set; } 
        public int CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; } 
        public int LastModifiedBy { get; set; } 
        public DateTime LastModifiedOn { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!; 
        public string? Description { get; set; }

        [Display(Name ="Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
