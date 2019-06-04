using EventOrganizer.BLL.Services;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EventOrganizer.Tests.ServiceTests
{
    public class EventRepositoryTests
    {
        private static readonly IEnumerable<Event> TestEvents = new[]
        {
            TestObjects.Event1,
            TestObjects.Event2,
            TestObjects.Event3
        };

        private static Mock<EventOrganizerDbContext> GetMockContext()
        {
            var mockContext = new Mock<EventOrganizerDbContext>();
            mockContext.Setup(item => item.Events).Returns(
                MockProvider.GetMockSet(TestEvents).Object
            );
            return mockContext;
        }

        [Fact]
        public void GetAllTest()
        {
            var list = TestEvents;
            var eventRepository = GetMockContext();
            var service = new EventService(eventRepository.Object);
            
            var actual = service.GetAll();

            Assert.Equal(list.ToList<Event>().ToString(), actual.ToList<Event>().ToString());

        }

        [Fact]
        public void GetEventsTest()
        {
            var list = TestEvents.Where(p => p.Category.Name.Equals("Bussiness")).OrderBy(p => p.Name);
            var eventRepository = GetMockContext();
            var service = new EventService(eventRepository.Object);

            var actual = service.GetEvents("Bussiness");

            Assert.Equal(list.ToList<Event>().ToString(), actual.ToList<Event>().ToString());

        }

        [Fact]
        public void GetEventsByUserIdTest()
        {
            var list = TestEvents.Where(e => e.UserId == "1").OrderBy(p => p.CreatedAt);
            var eventRepository = GetMockContext();
            var service = new EventService(eventRepository.Object);

            var actual = service.GetEventsByUserId("1");

            Assert.Equal(list.ToList<Event>().ToString(), actual.ToList<Event>().ToString());

        }

        [Fact]
        public void GetPreferredEventsTest()
        {
            var service = new EventService(GetMockContext().Object);
            var actual = service.GetPreferredEvents();
            var expected = TestEvents.Select(e => e.IsPreferredEvent);
            Assert.Equal(expected.Count(), actual.Count());
        }

        [Fact]
        public void GetEventByIdTest()
        {
            var mockContext = GetMockContext();
            var service = new EventService(mockContext.Object);
            var expected = mockContext.Object.Events.First();
            var actual = service.GetEventById(expected.Id);
            Assert.NotNull(actual);
        }

        [Fact]
        public void CreateTest()
        {
            var mockContext = GetMockContext();
            var service = new EventService(mockContext.Object);
            Assert.Equal(TestEvents.Count(), mockContext.Object.Events.Count());
            service.CreateItem(TestObjects.Event1);
            Assert.Equal(TestEvents.Count(), service.GetAll().Count());
        }

        [Fact]
        public void EditTest()
        {
            var mockContext = GetMockContext();
            var service = new EventService(mockContext.Object);
            var item = mockContext.Object.Events.First();
            service.EditItem(item);
            Assert.NotNull(service.GetEventById(item.Id));
        }

        [Fact]
        public void DeleteTest()
        {
            var mockContext = GetMockContext();
            var service = new EventService(mockContext.Object);
            var item = mockContext.Object.Events.First();
            service.DeleteItem(item);
            Assert.Null(mockContext.Object.Events.Find(item.Id));
        }
    }
}
