using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittlePacktBookstore.Models;
using LittlePacktBookstore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LittlePacktBookstore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly IRepository<Order> _orderRepo;

        public OrdersController(UserManager<SiteUser> userManager, IRepository<Order> orderRepo)
        {
            _userManager = userManager;
            _orderRepo = orderRepo;
        }
        public IActionResult Index()
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            if(User.IsInRole("Admin"))
            {
                ViewData["isAdmin"] = "true";
                return View(_orderRepo.GetAll());
            }
            else
            {
                ViewData["isAdmin"] = "false";
                return View(_orderRepo.GetAll().Where(x=>x.User.Id == user.Id));
            }
        }

        public IActionResult Details(int? id)
        {
            if(id==null || id<1)
            {
                return NotFound();
            }
            var order = _orderRepo.Get((int)id);
            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}