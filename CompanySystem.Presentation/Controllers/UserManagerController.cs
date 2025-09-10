using CompanySystem.DataAccessLayer.Models.Identity;
using CompanySystem.Presentation.ViewModels.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace CompanySystem.Presentation.Controllers
{
    public class UserManagerController : Controller
    {

        #region Services

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserManagerController> _logger;
        private readonly IWebHostEnvironment _environment;

        public UserManagerController(UserManager<ApplicationUser> userManager
            , ILogger<UserManagerController> logger
            , IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _logger = logger;
            _environment = environment;
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

            // Execute the query and get the list of users

            var userList = await query.ToListAsync();


            // Map users to ViewModel sequentially to avoid concurrency issues
            var users = new List<UserManagerViewModel>();
            foreach (var user in userList)
            {
                var roles = await _userManager.GetRolesAsync(user); // await each call sequentially
                users.Add(new UserManagerViewModel
                {
                    Id = user.Id,
                    FirstName = user.FName,
                    LastName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles
                });
            }

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

            if (user is null)
                return NotFound();

            // Map ApplicationUser to UserManagerViewModel
            var modelVM = new UserManagerViewModel
            {
                Id = user.Id,
                FirstName = user.FName,
                LastName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return View(modelVM);

        }



        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            // Map ApplicationUser to UserManagerViewModel
            var modelVM = new UserManagerViewModel
            {
                Id = user.Id,
                FirstName = user.FName,
                LastName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return View(modelVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserManagerViewModel userVM)
        {
            if (id != userVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(userVM);


            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            try
            {
                user.FName = userVM.FirstName;
                user.LName = userVM.LastName;
                user.Email = userVM.Email;
                user.PhoneNumber = userVM.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["Message"] = $"User ({user.FName}) has been updated successfully.";

                    return RedirectToAction("Index");

                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}", id);
                ModelState.AddModelError("", "Unexpected error while updating the user.");
            }
            return View(userVM);
        }

        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new UserManagerViewModel
            {
                Id = user.Id,
                FirstName = user.FName,
                LastName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            try
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Admin"))
                {
                    ModelState.AddModelError(string.Empty, "Admin users cannot be deleted.");
                    return View("Delete", new UserManagerViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FName,
                        LastName = user.LName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = roles
                    });
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    TempData["Message"] = $"User ({user.FName}) has been deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user {UserId}", id);
                var message = _environment.IsDevelopment()
                    ? ex.Message
                    : "Unexpected error while deleting the user.";
                ModelState.AddModelError(string.Empty, message);
            }

            // reload VM and return view if failed
            var modelVM = new UserManagerViewModel
            {
                Id = user.Id,
                FirstName = user.FName,
                LastName = user.LName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View("Delete", modelVM);
        }





        #endregion
    }

}
