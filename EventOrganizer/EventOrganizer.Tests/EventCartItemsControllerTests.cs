using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace EventOrganizer.Tests
{
    public class EventCartItemsControllerTests
    {
        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            return userManager;
        }
        private Mock<IEventService> GetEventServiceMock()
        {
            return new Mock<IEventService>();
        }
        private Mock<IEventCartItemsService> GetEventCartItemsServiceMock()
        {
            return new Mock<IEventCartItemsService>();
        }

        [Fact]
        public void ListViewTest()
        {
            var userManager = GetUserManagerMock();
            var eventService = GetEventServiceMock();
            var eventCartItemsService = GetEventCartItemsServiceMock();

            var eventCartItemsController = new EventCartItemsController(userManager.Object, eventService.Object, eventCartItemsService.Object);

            userManager.Setup(item => item.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(TestObjects.User1.Id);

            var cart = new EventCartItem[]
            {
                new EventCartItem()
                {
                    Id = 1,
                    UserId = "1",
                    EventId = 1
                }
            };
            eventCartItemsService.Setup(item => item.GetEventCartItems(It.IsAny<string>()))
                .Returns(cart);
            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);
            var result = eventCartItemsController.List();
            var eCIVW = new EventCartItemsViewModel()
            {
                EventCartItems = new List<Event>()
                {
                    TestObjects.Event1
                }
            };
            var expected = eCIVW.ToString();
            var actual = eventCartItemsController.ViewData.Model.ToString();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddToEventCartTest()
        {
            var userManager = GetUserManagerMock();
            var eventService = GetEventServiceMock();
            var eventCartItemsService = GetEventCartItemsServiceMock();

            var eventCartItemsController = new EventCartItemsController(userManager.Object, eventService.Object, eventCartItemsService.Object);
            userManager.Setup(item => item.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(TestObjects.User1.Id);

            var result = eventCartItemsController.AddToEventCart(TestObjects.Event1);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void RemoveFromEventCartTest()
        {
            var userManager = GetUserManagerMock();
            var eventService = GetEventServiceMock();
            var eventCartItemsService = GetEventCartItemsServiceMock();

            var eventCartItemsController = new EventCartItemsController(userManager.Object, eventService.Object, eventCartItemsService.Object);
            userManager.Setup(item => item.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(TestObjects.User1.Id);

            var result = eventCartItemsController.RemoveFromEventCart(TestObjects.Event1);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void ClearEventCartTest()
        {
            var userManager = GetUserManagerMock();
            var eventService = GetEventServiceMock();
            var eventCartItemsService = GetEventCartItemsServiceMock();

            var eventCartItemsController = new EventCartItemsController(userManager.Object, eventService.Object, eventCartItemsService.Object);
            userManager.Setup(item => item.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(TestObjects.User1.Id);

            var result = eventCartItemsController.ClearEventCart();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }


    }
}

