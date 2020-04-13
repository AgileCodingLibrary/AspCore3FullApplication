using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using LittlePacktBookstore.Services;
using LittlePacktBookstore.Models;
using LittlePacktBookstore.Controllers;
using Microsoft.AspNetCore.Mvc;
using LittlePacktBookstore.ViewModels;

namespace LittlePacktBookStore.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<IRepository<Book>> _mockBookRepo;
        private readonly Mock<IRepository<Carousel>> _mockCarouselRepo;
        private readonly Mock<IRepository<Order>> _mockOrderRepo;
        private readonly HomeController _sut;


        public HomeControllerTests()
        {
            _mockBookRepo = new Mock<IRepository<Book>>();
            _mockCarouselRepo = new Mock<IRepository<Carousel>>();
            _mockOrderRepo = new Mock<IRepository<Order>>();
            _sut = new HomeController(_mockBookRepo.Object, _mockCarouselRepo.Object,
                _mockOrderRepo.Object);
        }

        [Fact]
        public void ReturnViewForIndex()
        {
            IActionResult result = _sut.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void OrderReturnsCorrectViewNameModelType()
        {
            IActionResult result = _sut.Order(1);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.Model.GetType() == typeof(OrderViewModel));
            Assert.Equal("Order", viewResult.ViewName);
        }

        [Fact]
        public void OrderReturnsViewWhenModelStateIsInvalid()
        {
            _sut.ModelState.AddModelError("", "Dummy error");
            var order = new Order
            {
                BookId = 1,
                ClientName = "Ronnie",
                UserId = "",
                Country = "",
                Email = "",
                Zip = "",
                Street = "",
                City = "",
                State = "",
                Phone = ""
            };
            IActionResult result = _sut.Order(1, order);
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<OrderViewModel>(viewResult.Model);
            Assert.Equal(order.ClientName, model.OrderDetails.ClientName);
        }
    }
}
