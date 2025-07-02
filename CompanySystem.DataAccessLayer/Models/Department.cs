
namespace CompanySystem.DataAccessLayer.Models
{
    public class Department : BaseEntity
    {

        public string Name { get; set; } = null!; // Department Name 
        public string Code { get; set; } = null!;

        public string? Description { get; set; } // Department Description

    }
}
