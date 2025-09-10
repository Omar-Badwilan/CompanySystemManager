using System.ComponentModel.DataAnnotations;

namespace CompanySystem.Presentation.ViewModels.Managers.Roles
{
    public class RolesManagerViewModel
    {
        public string Id { get; set; } = null!;
        public string? RoleName { get; set; }
    }
}
