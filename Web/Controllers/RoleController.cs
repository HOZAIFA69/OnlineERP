using Core.Interfaces;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public IUnitOfWork<IdentityRole> _UnitOfWork { get; }

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork<IdentityRole> unitOfWork
                  )
        {
            this._roleManager = roleManager;
            this._UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult RolesList()
        {
            var roles = _UnitOfWork.Entity.GetAll();
            return View(roles);
        }


        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RolesViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(roleModel.roleName);
                if (role == null)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = roleModel.roleName
                    };
                    var result = await _roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(RoleController.RolesList), "Role");
                    }

                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }


                }
            }
            return View();
        }

        [HttpGet]
        public  ActionResult RoleDelete()
        {
            return View();
        }
            [HttpPost]
        public async Task<ActionResult> RoleDelete(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role == null)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = roleName
                    };
                    await _roleManager.DeleteAsync(identityRole);
                    return RedirectToAction(nameof(RoleController.RolesList), "Role");
                }

            }
            return View();
        }
    }
}
