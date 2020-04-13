using LittlePacktBookstore.Models;
using LittlePacktBookstore.Services;
using LittlePacktBookstore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittlePacktBookstore.Temp
{
    public class LandingPageHelper
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Carousel> _carousels;

        public LandingPageHelper(IRepository<Book> books, IRepository<Carousel> carousels)
        {
            _books = books;
            _carousels = carousels;
        }

        public HomeIndexViewModel GetIndexPageData()
        {
            return new HomeIndexViewModel
            {
                Books = GetBooks(),
                Carousels = GetCarousels()
            };
        }

        public List<Book> GetBooks()
        {
            return _books.GetAll().ToList();
        }

        public List<Carousel> GetCarousels()
        {
            return _carousels.GetAll().ToList();
        }

        public Book GetBookById(int id)
        {
            return _books.Get(id);
        }

        public Carousel GetCarouselById(int id)
        {
            return _carousels.Get(id);
        }

    }
}
