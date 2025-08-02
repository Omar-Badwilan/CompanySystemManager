using CompanySystem.BusinessLogic.DTOS.Employees;
using CompanySystem.BusinessLogic.Services.Employees;
using CompanySystem.DataAccessLayer.Models.Departments;
using CompanySystem.Presentation.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CompanySystem.Presentation.Controllers
{
    public class EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment) : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        #endregion

        #region Index
        [HttpGet]

        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
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
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            var message = string.Empty;

            try
            {
                var result = _employeeService.CreateEmployee(employeeDto);
                //if created then >0
                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee isn't created");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex,ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Employee isn't created";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeDto);

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
            return View(new EmployeeEditViewModel()
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
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeEditViewModel employeeVM)
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
                };
                var updated = _employeeService.UpdateEmployee(employeeToUpdate);

                if (updated > 0)
                    return RedirectToAction(nameof(Index));

                message = "Employee couldn't be updated";

            }
            catch (Exception ex)
            {
                //1. logger 
                _logger.LogError(ex, ex.Message);

                //2. Set
                message = _environment.IsDevelopment() ? ex.Message : "Employee couldn't be updated";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
        }


        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "Employee couldn't be deleted";
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Employee couldn't be deleted";
            }
            ModelState.AddModelError(string.Empty, message);
            return View();
        }
        #endregion
    }
}
