using AutoMapper;
using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.BusinessLogic.Services.Departments;
using CompanySystem.Presentation.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService
        , ILogger<DepartmentController> logger
        , IWebHostEnvironment environment
        ,IMapper mapper) : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly ILogger<DepartmentController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IMapper _mapper = mapper;
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
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM); // if there is error it returns the same view with the model state errors

            var message = string.Empty;
            try
            {

                /*                var createdDepartment = new CreatedDepartmentDto()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate

                };*/

                var createdDepartment =_mapper.Map<CreatedDepartmentDto>(departmentVM);

                var created = _departmentService.CreateDepartment(createdDepartment) > 0;

                if (created)
                {
                    TempData["Message"] = "Department is Created";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Department couldn't be created");

            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Department couldn't be Created";
                ModelState.AddModelError(string.Empty, message);
            }
            return View(departmentVM);

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

            var departmentVM = _mapper.Map<DepartmentDetailsDto, DepartmentViewModel>(department);

            return View(departmentVM);
        }

        [HttpPost] //Post: /Department/Edit
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid) //server side validation
                return View(departmentVM);

            var message = string.Empty;
            try
            {
                #region Manual
                /*                var departmentToUpdate = new UpdateDepartmentDto()
                        {
                            Id = id,
                            Code = departmentVM.Code,
                            Name = departmentVM.Name,
                            Description = departmentVM.Description,
                            CreationDate = departmentVM.CreationDate
                        };*/
                #endregion

                //var departmentToUpdate = _mapper.Map<UpdateDepartmentDto, DepartmentViewModel,>(departmentVM);

                var departmentToUpdate = _mapper.Map<UpdateDepartmentDto>(departmentVM);


                var updated = _departmentService.UpdateDepartment(departmentToUpdate) > 0;

                if (updated)
                {
                    TempData["Message"] = "Department is Updated";
                    return RedirectToAction(nameof(Index));
                }
                else
                    message = "Department couldn't be updated";
                
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Department couldn't be updated";
                ModelState.AddModelError(string.Empty, message);
            }
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
                {
                    TempData["Message"] = "Department is Deleted";
                    return RedirectToAction(nameof(Index));

                }
                message = "Department couldn't be Deleted";
            }
            catch (Exception ex)
            {
                //1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Department couldn't be deleted";
                ModelState.AddModelError(string.Empty, message);
            }
            return View();
        } 
        #endregion

    }
}
 