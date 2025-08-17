    using CompanySystem.BusinessLogic.DTOS.Employees;
    using CompanySystem.BusinessLogic.Services.Departments;
    using CompanySystem.BusinessLogic.Services.Employees;
    using CompanySystem.DataAccessLayer.Models.Departments;
    using CompanySystem.DataAccessLayer.Models.Employees;
    using CompanySystem.Presentation.ViewModels.Employees;
    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.AspNetCore.Mvc;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CompanySystem.Presentation.Controllers
{
    public class EmployeeController(IEmployeeService employeeService,
        ILogger<EmployeeController> logger, 
        IWebHostEnvironment environment) : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        #endregion

        #region Index
        [HttpGet]

        public IActionResult Index(string search)
        {
            var employees = _employeeService.GetEmployees(search);
            return View(employees);
        }

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id) {
            if (id is null)
                return BadRequest();
            var employee = _employeeService.GetEmployeesById(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);

            
        }
        #endregion

        #region Create
        //Create page [FORM]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //after Create button
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);
            var message = string.Empty;
            try
            {
                var createdEmployee = new CreatedEmployeeDto()
                {
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    HiringDate = employeeVM.HiringDate,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    DepartmentId = employeeVM.DepartmentId,
                };
                var created = _employeeService.CreateEmployee(createdEmployee) > 0;

                if (created)
                {
                    TempData["Message"] = "Employee is created successfully!";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Employee isn't created");
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex,ex.Message);
                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Employee isn't created";
                ModelState.AddModelError(string.Empty, message);
            }
            return View(employeeVM);
        }
        #endregion

        #region Update

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeesById(id.Value);

            if (employee is null)
                return NotFound();
            return View(new EmployeeViewModel()
            {
               Name = employee.Name,
               Address = employee.Address,
               Email = employee.Email,
               Age = employee.Age,
               Salary = employee.Salary,
               PhoneNumber = employee.PhoneNumber,
               IsActive = employee.IsActive,
               EmployeeType = employee.EmployeeType,
               Gender = employee.Gender,
               HiringDate = employee.HiringDate,
               DepartmentId = employee.DepartmentId,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)     
        {
            if (!ModelState.IsValid)
                return View(employeeVM);

            var message = string.Empty;
            try
            {
                var employeeToUpdate = new UpdateEmployeeDto()
                {
                    Id = id,
                    Name = employeeVM.Name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    HiringDate = employeeVM.HiringDate,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    DepartmentId = employeeVM.DepartmentId,
                };
                var updated = _employeeService.UpdateEmployee(employeeToUpdate) > 0;

                if (updated )
                {
                    TempData["Message"] = "Employee is updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Employee couldn't be updated");

            }
            catch (Exception ex)
            {
                //1. logger 
                _logger.LogError(ex, ex.Message);

                //2. Set
                message = _environment.IsDevelopment() ? ex.Message : "Employee couldn't be updated";
                ModelState.AddModelError(string.Empty, message);
            }
            return View(employeeVM);
        }


        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                {
                    TempData["Message"] = "Employee is deleted successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Employee couldn't be created");
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Employee couldn't be deleted";
                ModelState.AddModelError(string.Empty, message);
            }
            return View();
        }
        #endregion
    }
}
