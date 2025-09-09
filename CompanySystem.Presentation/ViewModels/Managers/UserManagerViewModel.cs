using System.ComponentModel.DataAnnotations;

namespace CompanySystem.Presentation.ViewModels.Managers
{
    public class UserManagerViewModel
    {
        public string Id { get; set; } = null!;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }


        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        public IList<string> Roles { get; set; }

    }
}
