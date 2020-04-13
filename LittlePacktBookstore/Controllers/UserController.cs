using LittlePacktBookstore.Models;
using LittlePacktBookstore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LittlePacktBookstore.Controllers
{   [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<SiteUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: User
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            var users = _userManager.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            });
            return View(users);
        }

        // GET: User/Details/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            var roles = _roleManager.Roles.ToList();
            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            var model = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                UserInRoles = roles.Select(x => new RolesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Selected = userRoles.Exists(y => y == x.Name)
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id, UserViewModel model)
        {
            var user = _userManager.Users.First(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();
            foreach (var item in model.UserInRoles)
            {
                if (item.Selected)
                {
                    await _userManager.AddToRoleAsync(user, roles.First(x => x.Id == item.Id).Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roles.First(x => x.Id == item.Id).Name);
                }
            }
            return RedirectToAction("Details", new { id });
        }
        // GET: User/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: User/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: User/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: User/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: User/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var user = _userManager.Users.First(x => x.Id == id);
            //await _userManager.SetLockoutEnabledAsync(user, false);
            //await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(100));
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        // POST: User/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult Profile()
        {
            var user = _userManager.Users.First(x => x.Email == User.Identity.Name);
            return View(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber
            });
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.First(x => x.Email == User.Identity.Name);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.UserName = model.Email;
                user.NormalizedUserName = model.Email.ToUpper();
                user.PhoneNumber = model.Phone;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    ViewData["message"] = "Successfully Updated Profile";
                }
                else
                {
                    ViewData["message"] = "Profile Update Error!";
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Password could not be changed.");
                    return View();
                }
                ModelState.AddModelError("", "Invalid data passed.");
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Password could not be changed due to an error.");
                return View();
            }
        }

    }
}