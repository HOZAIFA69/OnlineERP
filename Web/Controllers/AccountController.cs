using Core.Interfaces;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork<IdentityUser> _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUnitOfWork<IdentityRole> _unitOfRoles;

        public AccountController(
            IUnitOfWork<IdentityUser> unitOfWork,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUnitOfWork<IdentityRole> unitOfRoles)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._unitOfRoles = unitOfRoles;
        }

        [HttpGet]
        public async Task<IActionResult> UsersList()
        {
            return View(await _userManager.Users.ToListAsync());
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roles = new SelectList(_unitOfRoles.Entity.GetAll(), "Name", "Name");
            ViewBag.roles = roles;
            return View();
        }

        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {
            var roles = new SelectList(_unitOfRoles.Entity.GetAll(), "Name", "Name");
            ViewBag.roles = roles;
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = newUser.userName,
                    NormalizedUserName = newUser.fullName,
                    Email = newUser.Eamil
                };

                var result = await _userManager.CreateAsync(user, newUser.password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, newUser.Role);
                    // must return to user list later
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(newUser);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.userName,
                    PasswordHash = model.password
                };
                var result = await _signInManager.PasswordSignInAsync(model.userName, model.password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }


            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        [HttpGet]
        public async Task<IActionResult> RestPassword(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var restPasswordViewModel = new RestPasswordViewModel
                    {
                        Eamil = user.Email,
                        Token = token
                    };
                    return View(restPasswordViewModel);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RestPassword(RestPasswordViewModel restModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(restModel.Eamil);
                    if (user != null)
                    {
                        var result = await _userManager.ResetPasswordAsync(user, restModel.Token, restModel.password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(AccountController.UsersList), "Account");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }
    }


}
