using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetIdentityDeepDive.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentityDeepDive.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<MyIdentityUser> userManager;

        public HomeController(UserManager<MyIdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new MyIdentityUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };

                    var result = await userManager.CreateAsync(user, model.Password);
                }

                return View("Success");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View();
        }
    }
}