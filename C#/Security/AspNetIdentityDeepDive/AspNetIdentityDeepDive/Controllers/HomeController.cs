using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetIdentityDeepDive.Models;
using AspNetIdentityDeepDive.Models.Authentication;
using AspNetIdentityDeepDive.Models.Password;
using AspNetIdentityDeepDive.Models.TwoFactor;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AspNetIdentityDeepDive.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly IUserClaimsPrincipalFactory<MyIdentityUser> claimsPrincipalFactory;
        private readonly SignInManager<MyIdentityUser> signInManager;

        public HomeController(UserManager<MyIdentityUser> userManager,
            IUserClaimsPrincipalFactory<MyIdentityUser> claimsPrincipalFactory,
            SignInManager<MyIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.claimsPrincipalFactory = claimsPrincipalFactory;
            this.signInManager = signInManager;
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
                        UserName = model.UserName,
                        Email = model.UserName
                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationEmail = Url.Action("ConfirmEmailAddress", "Home",
                            new {token, email = user.Email}, Request.Scheme);
                        await System.IO.File.WriteAllTextAsync("confirmationLink.txt", confirmationEmail);
                    }
                }

                return View("Success");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return View("Success");
                }
            }

            return View("Error");
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

                if (user != null && !await userManager.IsLockedOutAsync(user))
                {
                    if (await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        if (!await userManager.IsEmailConfirmedAsync(user))
                        {
                            ModelState.AddModelError(string.Empty, "Email is not confirmed");
                            return View();
                        }

                        await userManager.ResetAccessFailedCountAsync(user);

                        // 2FA
                        if (await userManager.GetTwoFactorEnabledAsync(user))
                        {
                            var validProviders =
                                await userManager.GetValidTwoFactorProvidersAsync(user);

                            if (validProviders.Contains(userManager.Options.Tokens.AuthenticatorTokenProvider))
                            {
                                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                                    Store2FA(user.Id, userManager.Options.Tokens.AuthenticatorTokenProvider));

                                return RedirectToAction("TwoFactor");
                            }

                            if (validProviders.Contains("Email"))
                            {
                                var token =
                                    await userManager.GenerateTwoFactorTokenAsync(user, "Email");

                                await System.IO.File.WriteAllTextAsync("email2sv.txt", token);

                                await HttpContext.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                                    Store2FA(user.Id, "Email"));

                                return RedirectToAction("TwoFactor");
                            }
                        }

                        var principal = await claimsPrincipalFactory.CreateAsync(user);

                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);

                        return RedirectToAction("Index");
                    }

                    await userManager.AccessFailedAsync(user);

                    if (await userManager.IsLockedOutAsync(user))
                    {
                        // email the user, notifying them of lockout
                    }
                }

                /* If using SignInManager over UserManager */

                //var signInResult =
                //    await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                //if (signInResult.Succeeded)
                //{
                //    return RedirectToAction("Index");
                //}

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            return View();
        }

        private ClaimsPrincipal Store2FA(string userId, string provider)
        {
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("sub", userId),
                new Claim("amr", provider) // authentication method reference 
            }, IdentityConstants.TwoFactorUserIdScheme);

            return new ClaimsPrincipal(identity);
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPassword", "Home", new {token, email = user.Email},
                        Request.Scheme);

                    await System.IO.File.WriteAllTextAsync("resetLink.txt", resetUrl);
                }
                else
                {
                    // email user and inform them that they do not have an account
                }

                return View("Success");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetPasswordModel {Token = token, Email = email});
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }


                        return View();
                    }

                    if (await userManager.IsLockedOutAsync(user))
                    {
                        await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                    }

                    return View("Success");
                }

                ModelState.AddModelError(string.Empty, "Invalid Model Error");
            }

            return View();
        }

        [HttpGet]
        public IActionResult TwoFactor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TwoFactor(TwoFactorModel model)
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Your login request has expired");
                return View();
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(result.Principal.FindFirstValue("sub"));

                if (user != null)
                {
                    var isValid =
                        await userManager.VerifyUserTokenAsync(user, result.Principal.FindFirstValue("amr"),
                            "purpose", model.Token);

                    if (isValid)
                    {
                        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);

                        var claimsPrincipal = await claimsPrincipalFactory.CreateAsync(user);
                        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid token");
                    return View();
                }

                ModelState.AddModelError(string.Empty, "Invalid Request");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RegisterAuthenticator()
        {
            var user = await userManager.GetUserAsync(User);

            var authenticatorKey = await userManager.GetAuthenticatorKeyAsync(user);

            if (authenticatorKey == null)
            {
                await userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await userManager.GetAuthenticatorKeyAsync(user);
            }

            return View(new RegisterAuthenticatorModel {AuthenticatorKey = authenticatorKey});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RegisterAuthenticator(RegisterAuthenticatorModel model)
        {
            var user = await userManager.GetUserAsync(User);

            var isValid = await userManager.VerifyTwoFactorTokenAsync(user,
                userManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);

            if (!isValid)
            {
                ModelState.AddModelError(string.Empty, "Code is invalid");
                return View(model);
            }

            await userManager.SetTwoFactorEnabledAsync(user, true);
            return View("Success");
        }
    }
}
 
 