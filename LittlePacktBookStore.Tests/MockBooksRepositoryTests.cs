using LittlePacktBookstore.Services;
using System;
using System.Linq;
using Xunit;

namespace LittlePacktBookStore.Tests
{
    public class MockBooksRepositoryTests
    {
        [Fact]
        public void GetAllShouldReturnAllBooks()
        {
            var sysut = new MockBooksRepository();
            Assert.True(sysut.GetAll().ToList().Count() == 14);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void ShouldGetABookById(int id)
        {
            var sysut = new MockBooksRepository();
            Assert.True(sysut.Get(id) != null);
        }

        [Fact]
        public void GetBookWithOutOfRangeThrowsInvalidOperationException()
        {
            var sysut = new MockBooksRepository();
            Assert.Throws<InvalidOperationException>(()=>sysut.Get(20));
        }
        [Fact]
        public void GetBookWithNullValueThrowsArgumentNullException()
        {
            var sysut = new MockBooksRepository();
            Assert.Throws<ArgumentNullException>(() => sysut.Get(null));
        }
    }
}
