using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
      async  public Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,

            };

          var identityResult=  await userManager.CreateAsync(identityUser, registerViewModel.Password);
            if(identityResult.Succeeded)
            {
                //assign this user a "User" Role
              var roleIdentityResult=  await userManager.AddToRoleAsync(identityUser, "User");

                if(roleIdentityResult.Succeeded)
                {

                    return RedirectToAction("Register");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model= new LoginViewModel { ReturnUrl = ReturnUrl };
            return View(model);
        }
        [HttpPost]
       async public Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
          var signInResult= await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password,false,false);
            if(signInResult.Succeeded)
            {
                if(!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Logout()
        {
           await  signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
