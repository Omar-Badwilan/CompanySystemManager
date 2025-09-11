using Microsoft.AspNetCore.Mvc.Rendering;
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

        // Currently assigned roles
        public IList<string> Roles { get; set; } = new List<string>();

        // All roles available (to show in dropdown/checkboxes)
        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();

        // Roles selected in edit form
        public List<string> SelectedRoles { get; set; } = new List<string>();

    }
}
