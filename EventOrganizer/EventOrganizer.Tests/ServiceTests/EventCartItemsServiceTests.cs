using EventOrganizer.BLL.Services;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EventOrganizer.Tests.ServiceTests
{
    public class EventCartItemsServiceTests
    {

        private static Mock<IEventCartItemsRepository> GetMockContext()
        {
            var mockContext = new Mock<IEventCartItemsRepository>();
            return mockContext;
        }

        [Fact]
        public void AddToCartExistsTest()
        {
            var repository = GetMockContext();

            var service = new EventCartItemsService(repository.Object);
            repository.Setup(item => item.ItemExists(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true);
            service.AddToCart(1, "1");

            Assert.NotNull(service);
        }

        [Fact]
        public void AddToCartNotExistsTest()
        {
            var repository = GetMockContext();

            var service = new EventCartItemsService(repository.Object);
            repository.Setup(item => item.ItemExists(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(false);

            service.AddToCart(1, "1");

            Assert.NotNull(service);
        }

        [Fact]
        public void RemoveFromCartExistsTest()
        {
            var repository = GetMockContext();

            var service = new EventCartItemsService(repository.Object);
            repository.Setup(item => item.ItemExists(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true);
            service.RemoveFromCart(1, "1");

            Assert.NotNull(service);
        }

        [Fact]
        public void RemoveFromNotExistsTest()
        {
            var repository = GetMockContext();

            var service = new EventCartItemsService(repository.Object);
            repository.Setup(item => item.ItemExists(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(false);

            service.RemoveFromCart(1, "1");

            Assert.NotNull(service);
        }

        [Fact]
        public void GetEventCartItemsTest()
        {
            var repository = GetMockContext();

            var service = new EventCartItemsService(repository.Object);
            var citems = new EventCartItem[]
            {
                new EventCartItem()
                {
                    Id = 1,
                    EventId = 1,
                    UserId = "1"
                }
            };
            repository.Setup(item => item.GetAllItems(It.IsAny<string>()))
                .Returns(citems);

            var actual = service.GetEventCartItems("1");

            Assert.NotNull(service);
            Assert.Equal(citems.ToString(), actual.ToString());
        }

        [Fact]
        public void ClearCartTest()
        {
            var repository = GetMockContext();

            var service = new EventCartItemsService(repository.Object);
            
            service.ClearCart("1");

            Assert.NotNull(service);
        }


    }
}
