using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        public void ListTestEqual()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            Mock<IEventService> eventService = new Mock<IEventService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);

            var firstResult = eventsController.List("AAAAAAAAA");
            var secondResult = eventsController.List("All");

            Assert.Equal(firstResult.Model, secondResult.Model);
        }

        [Fact]
        public void ListTestIsType()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            Mock<IEventService> eventService = new Mock<IEventService>();

            EventsController eventsController = new EventsController(categoryService.Object, eventService.Object);

            var result = eventsController.List("Education");

            Assert.IsType<ViewResult>(result);
        }
    }
}
