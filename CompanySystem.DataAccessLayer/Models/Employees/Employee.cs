using CompanySystem.DataAccessLayer.Common.Enums;
using CompanySystem.DataAccessLayer.Models.Departments;
using System.ComponentModel.DataAnnotations;

namespace CompanySystem.DataAccessLayer.Models.Employees
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }


        public int? DepartmentId { get; set; }

        //Navigational property (OtO)
        public virtual Department? Department { get; set; }

        public string? Image { get; set; }

    }
}
