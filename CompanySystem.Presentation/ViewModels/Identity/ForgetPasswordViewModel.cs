using System.ComponentModel.DataAnnotations;

namespace CompanySystem.Presentation.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; } = null!;
    }
}
