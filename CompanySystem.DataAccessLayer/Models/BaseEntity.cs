
namespace CompanySystem.DataAccessLayer.Models
{
    public class BaseEntity
    {
        public int Id { get; set; } // pk
        public int CreatedBy { get; set; } //User Id

        public DateTime CreatedOn { get; set; } //time record was created on db

        public int LastModifiedBy { get; set; } //User Id

        public DateTime LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; } // Soft Delete
    }
}
