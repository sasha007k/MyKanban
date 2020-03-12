using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyKanban.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authservice)
        {
            this.authService = authservice;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginmodel)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.authService.SignIn(loginmodel);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Profile", "User");
                    //return this.RedirectToAction("AllProjects", "Project");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Login or password is incorrect!!");
                }
            }

            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await this.authService.SignOut();

            return this.RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View(new RegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (this.ModelState.IsValid && model != null)
            {
                if (await this.authService.FindByNameAsync(model.Email) == null)
                {
                    if ((await this.authService.Register(model)).Succeeded)
                    {
                        return this.RedirectToAction("Profile", "User");
                        //return this.RedirectToAction("AllProjects", "Project");
                    }
                }
            }

            return this.View();
        }

        public IActionResult AccessDenied()
        {
            return this.View();
        }
    }
}