using CompanySystem.DataAccessLayer.Models.Identity;
using CompanySystem.Presentation.ViewModels.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanySystem.Presentation.Controllers
{
    public class UserManagerController : Controller
    {

        #region Services

        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion


        public async Task<IActionResult> Index(string search)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.UserName.Contains(search) ||
                                                   u.FName.Contains(search) ||
                                                   u.LName.Contains(search));

            var users = await Task.WhenAll(query.ToList().Select(async user => new UserManagerViewModel
            {
                Id = user.Id,
                FirstName = user.FName,
                LastName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            }));

            // AJAX request → return only the partial
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("Partials/_UsersTablePartial", users);

            // Full request → return whole view
            return View(users);
        }
    }
}
