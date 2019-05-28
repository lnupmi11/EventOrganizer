using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace EventOrganizer.Tests
{
    public class EventsControllerTests
    {
        
        [Fact]
        public void ListTestIsNotNull()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            Mock<IEventService> eventService = new Mock<IEventService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);

            var result = eventsController.List("All");
            Assert.NotNull(result);
        }

        [Fact]
        public async void CreatePostTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);
            var result = await eventsController.Create(new ViewModels.CreateEventViewModel {});

            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void EditTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);
            var result = eventsController.Edit(TestObjects.Event1);

            categoryService.Setup(item => item.GetAll()).Returns(new List<Category>());
            Assert.NotNull(result.ViewData["Categories"]);
        }

        [Fact]
        public async void EditEventTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);
            var result = await eventsController.EditEvent(TestObjects.Event1);

            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void CreateTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);
            categoryService.Setup(item => item.GetAll()).Returns(new List<Category>());

            var result = eventsController.Create();
            Assert.NotNull(result.ViewData["Categories"]);
        }

        [Fact]
        public async void DeleteTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);
            var result = await eventsController.Delete(TestObjects.Event1);

            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void ListTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);
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

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);
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

    }
}
