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

            return View();
        }

        
        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
