using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Application.ViewModels.Account;
using BugTracker.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Application.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                throw new ArgumentException("Model state is invalid.");

            var emailExists = await _userManager.FindByEmailAsync(vm.Email);

            if (emailExists != null)
            {
                vm.Message = "Email already exists. Please choose a different email.";
                vm.Email = "";

                return View(vm);
            }

            var user = new AppUser { Email = vm.Email, UserName = vm.Email, FirstName = vm.FirstName, LastName = vm.LastName };
            var result = await _userManager.CreateAsync(user, vm.Password);

            if (result.Succeeded)
            {
                try
                {
                    await _signInManager.SignInAsync(user, false);
                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    return RedirectToAction("Index", "Project");
                }
                catch (Exception e)
                {
                    return RedirectToAction("LogIn");
                }
            }

            return View(vm);
        }

        public IActionResult LogIn()
        {
            return View(new LogInViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Message = "Model state is invalid. Please try again.";
                return View();
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(vm.Email);
                await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                HttpContext.Session.SetString("FirstName", user.FirstName);

                return RedirectToAction("Index", "Project");
            }
            catch (Exception e)
            {
                vm.Message = "An unexpected error occurred when attempting to log you in. Please try again.";
                return RedirectToAction("LogIn");
            }

        }
    }
}
