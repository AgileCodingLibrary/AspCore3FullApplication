using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittlePacktBookstore.Models;
using LittlePacktBookstore.Services;
using LittlePacktBookstore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LittlePacktBookstore.Controllers
{
    public class HomeController : Controller
    {
		IRepository<Book> _bookRepo;
		IRepository<Carousel> _CarouselRepo;
		IRepository<Order> _OrdersRepo;
		public HomeController(IRepository<Book> book, IRepository<Carousel> carousel,
			IRepository<Order> orders)
		{
			_bookRepo = book;
			_CarouselRepo = carousel;
			_OrdersRepo = orders;
		}
		//The home page
		public IActionResult Index()
        {
			HomeIndexViewModel model = new HomeIndexViewModel()
			{
				Books = _bookRepo.GetAll(),
				Carousels = _CarouselRepo.GetAll()
			};
			return View(model);
        }

        [Authorize]
		[HttpGet]
		public IActionResult AddBook()
		{
			return View();
		}

        [Authorize]
		[HttpPost]
		public IActionResult AddBook(Book book)
		{
			if(ModelState.IsValid)
			{
				Book item = new Book()
				{
					Id = _bookRepo.GetAll().Max(m => m.Id) + 1,
					Title = book.Title,
					Description = book.Description,
					Author = book.Author,
					PublishDate = book.PublishDate,
					Price = book.Price,
					image = book.image
				};
				_bookRepo.Add(item);
				return RedirectToAction("Index");
			}
			else
			{
				return View();
			}
		}

		//The about page
		public IActionResult About()
		{
			return View();
		}

		//The contact us page
		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Details(int id)
		{
			Book book = _bookRepo.Get(id);
			return View(book);
		}
		[HttpGet]
		public IActionResult Order(int? id)
		{
			if(id!=null && id>=0)
			{
				OrderViewModel model = new OrderViewModel()
				{
					BookToOrder = _bookRepo.Get((int)id),
					OrderDetails = new Order()
					{
						BookId = (int)id
					}
				};
				return View("Order", model);
			}
			return View();
		}
		[HttpPost]
		public IActionResult Order(int id, Order orderDetails)
		{
			if (ModelState.IsValid)
			{
				if (_bookRepo.GetAll().Count(x => x.Id == id) >= 1)
				{
					orderDetails.BookId = id;
					_OrdersRepo.Add(orderDetails);
					return RedirectToAction("ThankYou");
				}
				else
				{
					return View();
				}
			}
			else
			{
				return View(new OrderViewModel()
				{
					OrderDetails = orderDetails,
					BookToOrder = _bookRepo.Get(id)
				});
			}
		}

		public IActionResult ThankYou()
		{
			return View();
		}

		public IActionResult OrdersList()
		{
			
			return View(_OrdersRepo.GetAll());
		}
		[HttpGet]
		public IActionResult Register()
		{
			var model = new Registration
			{
				MailingAddress = new Address
				{
					Countries = new List<SelectListItem>
					{
						new SelectListItem{Value="Country1", Text="Country1", Selected=true},
						new SelectListItem{Value="Country2", Text="Country2"},
						new SelectListItem{Value="Country3", Text="Country3"},
						new SelectListItem{Value="Country4", Text="Country4"},
						new SelectListItem{Value="Country5", Text="Country5"}
					}
				}
			};
			return View("Register", model);
		}

		[HttpPost]
		public IActionResult Register(Registration registration)
		{
			return View("Register", registration);
		}

		public IActionResult CheckEmail(string email)
		{
			List<string> emails = new List<string>
			{
				"test1@test.com",
				"test2@test.com",
				"test3@test.com",
				"test4@test.com",
				"test5@test.com",
			};
			if (emails.Exists(x => x == email))
			{
				return Json("Email already exists.");
			}
			else
			{
				return Json(true);
			}
		}
	}
}