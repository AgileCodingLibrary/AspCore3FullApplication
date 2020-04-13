using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LittlePacktBookstore.Data;
using LittlePacktBookstore.Models;
using LittlePacktBookstore.Services;
using Microsoft.Extensions.Logging;

namespace LittlePacktBookstore.Controllers
{
    public class BooksController : Controller
    {
		private readonly IRepository<Book> _booksRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IRepository<Book> booksRepository, ILogger<BooksController> logger)
        {
            _booksRepository = booksRepository;
            _logger = logger;
        }

        // GET: Books
        public IActionResult Index()
        {
            return View(_booksRepository.GetAll());
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var book = _booksRepository.GetAll().FirstOrDefault(m => m.Id == (int)id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,PublishDate,Price,image")] Book book)
        {
            if (ModelState.IsValid)
            {
				_booksRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var book = _booksRepository.Get((int)id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Author,PublishDate,Price,image")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					_booksRepository.Edit(book);
                }
                catch (Exception ex)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var book = _booksRepository.Get((int)id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			var book = _booksRepository.Get(id);
			_booksRepository.Delete(book);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _booksRepository.Get(id)!=null;
        }
    }
}
