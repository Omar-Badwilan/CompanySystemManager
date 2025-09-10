using CompanySystem.DataAccessLayer.Models.Identity;
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
    }
}
