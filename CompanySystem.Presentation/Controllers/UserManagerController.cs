using CompanySystem.DataAccessLayer.Models.Identity;
using CompanySystem.Presentation.ViewModels.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CompanySystem.Presentation.Controllers
{
    public class UserManagerController : Controller
    {

        #region Services

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserManagerController> _logger;

        public UserManagerController(UserManager<ApplicationUser> userManager
            , ILogger<UserManagerController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        #endregion

        #region Index
        public async Task<IActionResult> Index(string search)
        {
            var query = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                var searchUpper = search.ToUpper();
                query = query.Where(u => u.UserName.ToUpper().Contains(searchUpper) ||
                                       u.FName.ToUpper().Contains(searchUpper) ||
                                       u.LName.ToUpper().Contains(searchUpper));
            }

            var userList = await query.ToListAsync(); // Fix: Use async

            var users = await Task.WhenAll(userList.Select(async user => new UserManagerViewModel
            {
                Id = user.Id,
                FirstName = user.FName,
                LastName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            }));

            return Request.Headers["X-Requested-With"] == "XMLHttpRequest"
                ? PartialView("Partials/_UsersTablePartial", users)
                : View(users);
        }
        #endregion

        #region Details
        [HttpGet]

        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);

            if(user is null)
                return NotFound();

            // Map ApplicationUser to UserManagerViewModel
            var model = new UserManagerViewModel
            {
                Id = user.Id,
                FirstName = user.FName,   
                LastName = user.LName,    
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return View(model);

        }



        #endregion

    }
}
