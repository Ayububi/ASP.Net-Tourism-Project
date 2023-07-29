using Microsoft.AspNetCore.Mvc;
using Tour_FP.Models.DTO;
using Tour_FP.Repositories.Abstract;
using Tour_FP.Repositories.Implementation;

namespace Tour_FP.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private IUserAuthenticationServices authService;
        public UserAuthenticationController(IUserAuthenticationServices authService)
        {
            this.authService = authService;
        }
        /* We will create a user with admin rights, after that we are going
          to comment this method because we need only
          one user in this application 
         */
        public async Task<IActionResult> Register()
        {
            var model = new RegistrationModel
            {
                Email = "admin@gmail.com",
                Username = "admin",
                Name = "Ayub",
                Password = "Admin@123",
                PasswordConfirm = "Admin@123",
                Role = "Admin"
            };
            // if you want to register with user , Change Role="User"
            var result = await authService.RegisterAsync(model);
            return Ok(result.Message);
        }

        public async Task<IActionResult> Login()
        {
            string returnUrl = Request.Query["ReturnUrl"];
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            
                

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return LocalRedirect(returnUrl); // Redirect to the original page after login if it's a local URL
                else
                    return RedirectToAction("Index", "Home"); // Redirect to the home page if returnUrl is not provided or is not a local URL
            }
            else
            {
                TempData["msg"] = "Could not log in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationModel model)
        {
            model.Role = "User"; // Set the role to "User" for sign-up
            


                var result = await authService.RegisterAsync(model);
                if (result.StatusCode == 1)
                {
                    TempData["msg"] = "Registration successful. You can now log in with your credentials.";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    TempData["msg"] = "Could not register user.";
                    return View(model);
                }
            
           
                

        }
    }
}
