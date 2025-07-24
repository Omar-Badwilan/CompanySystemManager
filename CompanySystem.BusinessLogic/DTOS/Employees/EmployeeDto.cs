using CompanySystem.DataAccessLayer.Common.Enums;
using System.ComponentModel.DataAnnotations;


namespace CompanySystem.BusinessLogic.DTOS.Employees
{
    public class EmployeeDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]   
        public bool IsActive { get; set; } = true;

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public string? Gender { get; set; }

        public string? EmployeeType { get; set; }


    }
}
