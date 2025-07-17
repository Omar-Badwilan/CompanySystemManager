using System.ComponentModel.DataAnnotations;


namespace CompanySystem.BusinessLogic.DTOS.Departments
{
    public class DepartmentDto
    {
        public int Id { get; set; } // Pk
        public string Name { get; set; } = null!; // Department Name 
        public string Code { get; set; } = null!;

        [Display(Name ="Date of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
