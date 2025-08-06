using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.BusinessLogic.Services.Departments;
using CompanySystem.Presentation.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment) : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly ILogger<DepartmentController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        #endregion

        #region Index
        [HttpGet]//GET: /Department/Index   
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        #endregion

        #region Details
        [HttpGet] //GET: /Department/Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentsById(id.Value);

            if (department is null)
                return NotFound();
            return View(department);
        }
        #endregion

        #region Create
        [HttpGet] //GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        //when do create button
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedDepartmentDto departmentdto)
        {
            if (!ModelState.IsValid)
                return View(departmentdto); // if there is error it returns the same view with the model state errors

            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(departmentdto);

                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Department isn't Created");
                    return View(departmentdto);
                }
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Department isn't Created";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentdto);

        }
        #endregion

        #region Update
        [HttpGet] //GET: /Department/Edit/id?
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest(); // 400

            var department = _departmentService.GetDepartmentsById(id.Value);

            if (department is null)
                return NotFound(); // 404

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });
        }

        [HttpPost] //Post: /Department/Edit
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid) //server side validation
                return View(departmentVM);

            var message = string.Empty;
            try
            {
                var departmentToUpdate = new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate

                };

                var updated = _departmentService.UpdateDepartment(departmentToUpdate) > 0;

                if (updated)
                    return RedirectToAction(nameof(Index));

                message = "Department couldn't be updated";
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Department couldn't be updated";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }
        #endregion

        #region Delete
        [HttpGet] //GET: /Department/Delete/id?
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentsById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpPost] //Post 
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "Department couldn't be deleted";
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Department couldn't be deleted";

            }

            ModelState.AddModelError(string.Empty, message);
            return View();
        } 
        #endregion

    }
}
 