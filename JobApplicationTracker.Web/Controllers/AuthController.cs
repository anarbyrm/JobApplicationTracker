using JobApplicationTracker.Application.Models;
using identity = Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JobApplicationTracker.Web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly identity.SignInManager<identity.IdentityUser> _signInManager;

        public AuthController(identity.SignInManager<identity.IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("login")]
        public IActionResult Login()
        {
            LogInModel logInModel = new();
            return View(logInModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LogInModel input)
        {
            identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                input.Username, input.Password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is invalid");
                return View(input);
            }

            return RedirectToAction("List", "JobApplication");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("register")]
        public IActionResult Register()
        {
            RegisterModel userCreateModel = new();
            return View(userCreateModel);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromForm] RegisterModel input,
            [FromServices] identity.UserManager<identity.IdentityUser> userManager)
        {
            var user = new identity.IdentityUser(userName: input.Username);
            var result = await userManager.CreateAsync(user, password: input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(input);
            }
            TempData["SuccessMessage"] = "User has successfully been created.";
            return RedirectToAction("Login");
        }
    }
}
