namespace CompanySystem.BusinessLogic.DTOS.Departments
{
    public class CreatedDepartmentDto
    {
        public string Name { get; set; } = null!; // Department Name 
        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!; // Department Description

        public DateOnly CreationDate { get; set; } // time department was created
    }
}
