using CompanySystem.DataAccessLayer.Models.Employees;

namespace CompanySystem.DataAccessLayer.Models.Departments
{
    public class Department : BaseEntity
    {
        //public int CreatedBy { get; set; } 
        //public DateTime CreatedOn { get; set; }
        //public int LastModifiedBy { get; set; } 
        //public DateTime LastModifiedOn { get; set; }
        public string Name { get; set; } = null!; // Department Name 
        public string Code { get; set; } = null!;
        public string? Description { get; set; } //Department Description
        public DateOnly CreationDate { get; set; } // time department was created

        //Navigatonal property [Many]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
