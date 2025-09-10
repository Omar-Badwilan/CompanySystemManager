using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.Presentation.ViewModels.Departments;
using CompanySystem.Presentation.ViewModels.Managers.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Presentation.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleManagerController> _logger;
        private readonly IWebHostEnvironment _environment;

        #region Services
        public RoleManagerController(RoleManager<IdentityRole> roleManager
            , ILogger<RoleManagerController> logger
            , IWebHostEnvironment environment)
        {
            _roleManager = roleManager;
            _logger = logger;
            _environment = environment;
        }


        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var query = _roleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                var searchUpper = search.ToUpper();

                query = query.Where(r => r.Name.ToUpper().Contains(searchUpper));
            }

            // Execute the query and get the list of roles
            var roleList = await query.ToListAsync();

            var roles = roleList.Select(r => new RolesManagerViewModel
            {
                Id = r.Id,
                RoleName = r.Name
            }).ToList();


            return Request.Headers["X-Requested-With"] == "XMLHttpRequest"
     ? PartialView("Partials/_RolesTablePartial", roles)
     : View(roles);

        }

        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
                return BadRequest();
            var user = await  _roleManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            // Map IdentityRole to RoleManagerView
            var modelVM = new RolesManagerViewModel
            {
                Id = user.Id,
                RoleName = user.Name,
            };

            return View(modelVM);

        }

        #endregion

        #region Create
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolesManagerViewModel rolesVM)
        {
            if (!ModelState.IsValid)
                return View(rolesVM); // if there is error it returns the same view with the model state errors

            try
            {

                var createdRole = new IdentityRole()
                {
                    //Id is created Automatically
                    Name = rolesVM.RoleName,
                };

                var result = await _roleManager.CreateAsync(createdRole);
                if (result.Succeeded)
                {
                    // Now Id is generated
                    TempData["Message"] = "Role Is Created";

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var message = _environment.IsDevelopment() ? ex.Message : "Role couldn't be Created";
                ModelState.AddModelError(string.Empty, message);
            }
            return View(rolesVM);

        }


        #endregion
    }
}
