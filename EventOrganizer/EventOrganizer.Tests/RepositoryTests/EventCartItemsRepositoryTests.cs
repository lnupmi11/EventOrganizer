using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using EventOrganizer.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EventOrganizer.Tests.RepositoryTests
{
    public class EventCartItemsRepositoryTests
    {
        private static readonly IEnumerable<EventCartItem> TestCart = new[]
        {
            new EventCartItem()
            {
                Id = 1,
                EventId = 1,
                UserId = "1"
            },
            new EventCartItem()
            {
                Id = 2,
                EventId = 2,
                UserId = "2"
            },
            new EventCartItem()
            {
                Id = 3,
                EventId = 3,
                UserId = "3"
            }
        };

        private static Mock<EventOrganizerDbContext> GetMockContext()
        {
            var mockContext = new Mock<EventOrganizerDbContext>();
            mockContext.Setup(item => item.EventCartItems)
                .Returns(MockProvider.GetMockSet(TestCart).Object);
            return mockContext;
        }

        [Fact]
        public void EventCartItemsTest()
        {
            var repo = new EventCartItemsRepository(GetMockContext().Object);
            var res = repo.EventCartItems;

            Assert.Equal(res.ToList<EventCartItem>().ToString(), TestCart.ToList<EventCartItem>().ToString());
        }

        [Fact]
        public void AddItemExistsTest()
        {
            var mock = GetMockContext();
            var repo = new EventCartItemsRepository(mock.Object);
            repo.AddItem(1, "1");
            Assert.NotNull(repo);
        }

        [Fact]
        public void AddItemTest()
        {
            var mock = GetMockContext();
            var repo = new EventCartItemsRepository(mock.Object);
            repo.AddItem(1, "3");
            Assert.NotNull(repo);
        }

        [Fact]
        public void RemoveItemThatNotExistsTest()
        {
            var mock = GetMockContext();
            var repo = new EventCartItemsRepository(mock.Object);
            repo.RemoveItem(1, "2");
            Assert.NotNull(repo);
        }
        [Fact]
        public void RemoveItemTest()
        {
            var mock = GetMockContext();
            var repo = new EventCartItemsRepository(mock.Object);
            repo.RemoveItem(1, "1");
            Assert.NotNull(repo);
        }

        [Fact]
        public void ItemExistsTest()
        {
            var mock = GetMockContext();
            var repo = new EventCartItemsRepository(mock.Object);
            Assert.NotNull(repo);
            var result = repo.ItemExists(1, "1");
            Assert.True(result);
            result = repo.ItemExists(1, "2");
            Assert.False(result);
        }

        [Fact]
        public void GetAllItemsTest()
        {
            var mock = GetMockContext();
            var repo = new EventCartItemsRepository(mock.Object);
            var result = repo.GetAllItems("1");
            var expected = TestCart.Where(s => s.UserId == "1").ToList();
            Assert.Equal(expected.ToString(),result.ToString());
        }

        [Fact]
        public void RemoveAllItemsTest()
        {
            var mock = GetMockContext();
            var repo = new EventCartItemsRepository(mock.Object);
            repo.RemoveAllItems("1");
            Assert.NotNull(repo);
        }

    }
}
