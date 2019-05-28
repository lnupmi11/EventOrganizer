using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace EventOrganizer.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexTest()
        {
            Mock<IEventService> eventService = new Mock<IEventService>();

            HomeController homeController = new HomeController(eventService.Object);
            eventService.Setup(item => item.GetPreferredEvents()).Returns
            (
                new List<Event>() {TestObjects.Event1, TestObjects.Event2, TestObjects.Event3 }
            );
            var resultModel = homeController.Index().Model;
            var expected = new HomeViewModel
            {
                PreferredEvents = new List<Event>() { TestObjects.Event1, TestObjects.Event2, TestObjects.Event3 }
            };
            Assert.Equal(resultModel.ToString(), expected.ToString());
        }
        
    }
}
