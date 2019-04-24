using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using Moq;
using Xunit;

namespace EventOrganizer.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexTestNotNull()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();

            HomeController homeController = new HomeController(eventService.Object);

            var result = homeController.Index();
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexTestModelNotNull()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();

            HomeController homeController = new HomeController(eventService.Object);

            var result = homeController.Index();
            Assert.NotNull(result.Model);
        }

        [Fact]
        public void IndexTestModelNameTrue()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();

            HomeController homeController = new HomeController(eventService.Object);

            var result = homeController.Index();
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }
    }
}
