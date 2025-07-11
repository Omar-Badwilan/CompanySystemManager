using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.BusinessLogic.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger,IWebHostEnvironment environment) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly ILogger<DepartmentController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;

        [HttpGet]//GET: /Department/Index   
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }

        [HttpGet] //GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        //when do create button
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(department);

                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Department isn't Created");
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message

                if(_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else 
                {
                    message = "Department isn't created";
                    return View("Error",message);
                }



            }

        }
    }
}
