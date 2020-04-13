using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using LittlePacktBookstore.Services;
using LittlePacktBookstore.Models;
using System.Linq;
using LittlePacktBookstore.Temp;
using LittlePacktBookstore.ViewModels;

namespace LittlePacktBookStore.Tests
{
    public class LandingPageHelperTests
    {
        [Fact]
        public void GetIndexPageDataReturnsHomeIndexViewModel()
        {
            var mockBook = new Mock<IRepository<Book>>();
            mockBook.Setup(x => x.GetAll()).Returns(GetBooks());
            var mockCarousel = new Mock<IRepository<Carousel>>();
            mockCarousel.Setup(x => x.GetAll()).Returns(GetCarousels());
            var sysut = new LandingPageHelper(mockBook.Object, mockCarousel.Object);
            var result = sysut.GetIndexPageData();
            Assert.True(result.Books.Count() == 14);
            Assert.True(result.Carousels.Count() == 3);
            Assert.True(result.GetType() == typeof(HomeIndexViewModel));
        }

        private List<Carousel> GetCarousels()
        {
            return new MockCarouselRepository().GetAll().ToList();
        }

        private List<Book> GetBooks()
        {
            return new MockBooksRepository().GetAll().ToList();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetBookByIdReturnsBook(int id)
        {
            var books = GetBooks();
            var mockBook = new Mock<IRepository<Book>>();
            mockBook.Setup(x => x.Get(It.Is<int>(y=>y<0 || y>13)))
                .Throws(new InvalidOperationException());
            mockBook.Setup(x => x.Get(It.Is<int>(y => y >= 0 && y <= 13)))
                .Returns(books[id]);
            var mockCarousel = new Mock<IRepository<Carousel>>();
            var sysut = new LandingPageHelper(mockBook.Object, mockCarousel.Object);
            var result = sysut.GetBookById(id);
            Assert.True(result != null);
            Assert.Throws<InvalidOperationException>(() => sysut.GetBookById(20));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void GetCarouselByIdReturnsCarousel(int id)
        {
            var carousels = GetCarousels();
            var mockBook = new Mock<IRepository<Book>>();
            var mockCarousel = new Mock<IRepository<Carousel>>();
            mockCarousel.Setup(x => x.Get(It.Is<int>(y => y < 0 || y > 3)))
                .Throws(new InvalidOperationException());
            mockCarousel.Setup(x => x.Get(It.Is<int>(y => y >= 0 && y <= 3)))
                .Returns(carousels.FirstOrDefault(z => z.Id == id));
            var sut = new LandingPageHelper(mockBook.Object, mockCarousel.Object);
            var res = sut.GetCarouselById(id);
            Assert.True(res != null);
            Assert.Throws<InvalidOperationException>(() => sut.GetCarouselById(20));
        }
    }
}
