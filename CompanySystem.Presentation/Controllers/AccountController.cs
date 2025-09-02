using CompanySystem.DataAccessLayer.Models.Identity;
using CompanySystem.Presentation.Utilites;
using CompanySystem.Presentation.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Presentation.Controllers
{
    public class AccountController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region SignUp

        [HttpGet] //GET : Account/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost] //POST
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is { })
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This username is already in use for another account.");
                return View(model);
            }
            user = new ApplicationUser()
            {
                FName = model.FirstName,
                LName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgree = model.IsAgree,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(SignIn));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }
        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is { })
            {
                var flag = await _userManager.CheckPasswordAsync(user, model.Password);

                if (flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account isn't confirmed yet!!");

                    if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your account is locked!!");

                    //if (result.RequiresTwoFactor)
                    //{

                    //}

                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                }

            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
            return View(model);
        }

        #endregion

        #region Sign Out

        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region ForgetPassword

        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);

                if(user is { })
                {
                    //Send Email
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = "Reset Password Link", // Lesa htt3ml
                    };

                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword) ,viewModel);
        }

        [HttpGet]

        public IActionResult CheckYourInbox() => View();

        #endregion
    }
}

