using System.ComponentModel.DataAnnotations;

namespace CompanySystem.Presentation.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Is required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
