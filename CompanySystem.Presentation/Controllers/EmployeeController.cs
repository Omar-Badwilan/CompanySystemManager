using AutoMapper;
using CompanySystem.BusinessLogic.DTOS.Employees;
using CompanySystem.BusinessLogic.Services.Employees;
using CompanySystem.Presentation.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    [Authorize]
    public class EmployeeController(IEmployeeService employeeService,
        ILogger<EmployeeController> logger, 
        IWebHostEnvironment environment,
        IMapper mapper) : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IMapper _mapper = mapper;
        #endregion

        #region Index
        [HttpGet]

        public async Task<IActionResult> Index(string search)
        {
            var employees = await _employeeService.GetEmployeesAsync(search);

            // Check if it's an AJAX request
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                // Return only the partial view for AJAX
                return PartialView("Partials/_EmployeesTablePartial", employees);

            // Otherwise, return the full page
            return View(employees);
        }

        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id) {
            if (id is null)
                return BadRequest();
            var employee = await _employeeService.GetEmployeesByIdAsync(id.Value);

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

        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid) //serverside validation
                return View(employeeVM);
            var message = string.Empty;
            try
            {

                var createdEmployee =_mapper.Map<CreatedEmployeeDto>(employeeVM);

                var created = await _employeeService.CreateEmployeeAsync(createdEmployee) > 0;

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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = await _employeeService.GetEmployeesByIdAsync(id.Value);

            if (employee is null)
                return NotFound();

            var employeeVm = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)     
        {
            if (!ModelState.IsValid)
                return View(employeeVM);

            var message = string.Empty;
            try
            {
/*                var employeeToUpdate = new UpdateEmployeeDto()
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
                };*/

                var employeeToUpdate = _mapper.Map<UpdateEmployeeDto>(employeeVM);
                var updated = await _employeeService.UpdateEmployeeAsync(employeeToUpdate) > 0;

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
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _employeeService.DeleteEmployeeAsync(id);
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
