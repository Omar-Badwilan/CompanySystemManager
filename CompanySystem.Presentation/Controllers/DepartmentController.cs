using CompanySystem.BusinessLogic.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService) : Controller
    {

        private readonly IDepartmentService _departmentService = departmentService;

        public IActionResult Index()
      {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
    }
}
