using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittlePacktBookstore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LittlePacktBookstore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        // GET: Role
        public ActionResult Index()
        {
            IEnumerable<RolesViewModel> model = _roleManager.Roles.Select(x => new RolesViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });
            return View(model);
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RolesViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = model.Name
                });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public async Task<ActionResult> Edit(string name)
        {
            var result = await _roleManager.FindByNameAsync(name);        
            return View( new RolesViewModel
            {
                Id = result.Id,
                Name = result.Name
            });
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string name)
        {
            try
            {  
                if(await _roleManager.RoleExistsAsync(name))
                {
                    ModelState.AddModelError("", "A role with that name already exists");
                    return View();
                }
                else
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = name;
                    role.NormalizedName = name.ToUpper();
                    var result = await _roleManager.UpdateAsync(role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Couldn't update the role.");
                        return View();
                    }
                }
                // TODO: Add update logic here

                
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public async Task<ActionResult> Delete(string name)
        {
            try
            {
                if(!await _roleManager.RoleExistsAsync(name))
                {
                    ModelState.AddModelError("", "Accepted role with that name doesn't exist");
                    return View();
                }
                else
                {
                    var role = await _roleManager.FindByNameAsync(name);
                    return View(new RolesViewModel
                    {
                        Id = role.Id,
                        Name = role.Name
                    });                    
                }
            }
            catch (Exception)
            {
                // log the error here
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Role/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, IFormCollection collection)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if(role!=null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", $"The {role.Name} could not be deleted.");
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}