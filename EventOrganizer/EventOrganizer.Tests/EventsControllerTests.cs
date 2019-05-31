using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace EventOrganizer.Tests
{
    public class EventsControllerTests
    {
        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            return userManager;
        }
        [Fact]
        public void ListTestIsNotNull()
        {
            var categoryService = new Mock<ICategoryService>();
            var eventService = new Mock<IEventService>();
            var userManager = GetUserManagerMock();
            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);

            var result = eventsController.List("All");
            Assert.NotNull(result);
        }

        [Fact]
        public async void CreatePostTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            var result = await eventsController.Create(new ViewModels.CreateEventViewModel {});

            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void EditUserIsAuthorTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            userManager.Setup(item => item.GetUserId(null))
                .Returns(TestObjects.User1.Id);
            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);
            var result = eventsController.Edit(TestObjects.Event1.Id);
            categoryService.Setup(item => item.GetAll()).Returns(new List<Category>());
            Assert.NotNull(result.ViewData["Categories"]);
            var expected = new SelectList(new List<Category>().ToString(), "Id", "Name").ToString();
            var actual = result.ViewData["Categories"].ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EditUserIsNotAuthorTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);
            userManager.Setup(item => item.GetUserId(null))
                .Returns(TestObjects.User2.Id);
            var result = eventsController.Edit(TestObjects.Event1.Id);
            categoryService.Setup(item => item.GetAll()).Returns(new List<Category>());
            Assert.Null(result.ViewData["Categories"]);
        }


        [Fact]
        public async void EditEventTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);
            userManager.Setup(item => item.GetUserId(null))
                .Returns(TestObjects.User1.Id);
            var result = await eventsController.EditEvent(TestObjects.Event1);

            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void CreateTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            categoryService.Setup(item => item.GetAll()).Returns(new List<Category>());

            var result = eventsController.Create();
            Assert.NotNull(result.ViewData["Categories"]);
        }

        [Fact]
        public async void DeleteUserIsAdminTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();
            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);
            userManager.Setup(item => item.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(TestObjects.User1);
            userManager.Setup(item => item.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            
            var result = await eventsController.Delete(TestObjects.Event1.Id);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void DeleteUserIsOfficialTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();
            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);
            userManager.Setup(item => item.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(TestObjects.User1);
            userManager.Setup(item => item.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);

            var result = await eventsController.Delete(TestObjects.Event1.Id);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void DeleteUserHaveNoAccessTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();
            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);
            userManager.Setup(item => item.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(TestObjects.User2);
            userManager.Setup(item => item.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(false);
            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);

            var result = await eventsController.Delete(TestObjects.Event1.Id);
            Assert.IsType<ForbidResult>(result);
        }


        [Fact]
        public void ListTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            categoryService.Setup(item => item.GetCategoryName(TestObjects.Category1.Name)).Returns(
                TestObjects.Category1.Name
            );
            eventService.Setup(item => item.GetAll()).Returns(
                new List<Event>() { TestObjects.Event1, TestObjects.Event2, TestObjects.Event3 }
            );
            eventService.Setup(item => item.GetEvents(TestObjects.Category1.Name)).Returns(
                new List<Event>() { TestObjects.Event1 }
            );

            var result = eventsController.List(TestObjects.Category1.Name);
            var expected = new EventsListViewModel()
            {
                Events = new List<Event>() { TestObjects.Event1 },
                CurrentCategory = TestObjects.Category1.Name
            };
            Assert.Equal(expected.ToString(), result.Model.ToString());
        }

        [Fact]
        public void ListInvalidCategoryTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            categoryService.Setup(item => item.GetCategoryName("invalid_category")).Returns(
                "All"
            );
            eventService.Setup(item => item.GetAll()).Returns(
                new List<Event>() { TestObjects.Event1, TestObjects.Event2, TestObjects.Event3 }
            );
            eventService.Setup(item => item.GetEvents("invalid_category")).Returns(
                new List<Event>() {}
            );

            var result = eventsController.List("invalid_category");
            var expected = new EventsListViewModel()
            {
                Events = new List<Event>() { TestObjects.Event1, TestObjects.Event2, TestObjects.Event3 },
                CurrentCategory = "All"
            };
            Assert.Equal(expected.ToString(), result.Model.ToString());
        }

        [Fact]
        public void ShowTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);

            eventService.Setup(item => item.GetEventById(It.IsAny<int>()))
                .Returns(TestObjects.Event1);

            var result = eventsController.Show(TestObjects.Event1.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async void InvalidStatesTestAsync()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            var userManager = GetUserManagerMock();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object, userManager.Object);
            Event ev = null;

            eventsController.ModelState.AddModelError("invalid", "Invalid model state");
            var result = await eventsController.EditEvent(ev);
            Assert.NotNull(result);
            Assert.False(eventsController.ModelState.IsValid);

            result = await eventsController.Create(new CreateEventViewModel());
            Assert.NotNull(result);
            Assert.False(eventsController.ModelState.IsValid);

            result = await eventsController.Create(new CreateEventViewModel());
            Assert.NotNull(result);
            Assert.False(eventsController.ModelState.IsValid);

            result = await eventsController.Delete(TestObjects.Event1.Id);
            Assert.NotNull(result);
            Assert.False(eventsController.ModelState.IsValid);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

    }
}
