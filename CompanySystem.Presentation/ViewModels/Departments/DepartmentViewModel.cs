using System.ComponentModel.DataAnnotations;

namespace CompanySystem.Presentation.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        [Required(ErrorMessage = "Code is Required !")]
        public string Name { get; set; } = null!; // Department Name 
        public string Code { get; set; } = null!;
        public string? Description { get; set; } // Department Description

        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
