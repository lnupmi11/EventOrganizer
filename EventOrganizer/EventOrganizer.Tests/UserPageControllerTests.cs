using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using EventOrganizer.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace EventOrganizer.Tests
{
    public class UserPageControllerTests
    {
        private static readonly IEnumerable<User> TestUsers = new[]
        {
            TestObjects.User1,
            TestObjects.User2,
            TestObjects.User3
        };
        private static readonly IEnumerable<Event> TestEvents = new[]
        {
            TestObjects.Event1,
            TestObjects.Event2,
            TestObjects.Event3
        };
        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            return userManager;
        }
        [Fact]
        public void IndexTestNotNull()
        {
            var eventService = new Mock<IEventService>();
            var userManager = GetUserManagerMock();
            UserPageController userPageController = new UserPageController(eventService.Object, userManager.Object);
            userManager.Setup(item => item.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1");
            eventService.Setup(item => item.GetEventsByUserId(It.IsAny<string>())).Returns(TestEvents);

            var result = userPageController.Index();
            var actual = userPageController.ViewData["UserEvents"].ToString();
            var expected = TestEvents.ToString();

            Assert.Equal(expected, actual);
            Assert.NotNull(result);
        }
    }
}
