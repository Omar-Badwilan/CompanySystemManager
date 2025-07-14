using System.ComponentModel.DataAnnotations;

namespace CompanySystem.BusinessLogic.DTOS.Departments
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage ="Code is Required!!")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!; // Department Name 

        public string? Description { get; set; } // Department Description

        [Display(Name ="Date Of Creation")]
        public DateOnly CreationDate { get; set; } // time department was created
    }
}
