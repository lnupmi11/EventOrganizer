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
        public void GetEventsTest()
        {
            var repository = new EventRepository(GetMockContext().Object);
            var actual = repository.Events;
            Assert.Equal(TestEvents.Count(), actual.Count());
        }

        [Fact]
        public void PreferredEventsTest()
        {
            var repository = new EventRepository(GetMockContext().Object);
            var actual = repository.PreferredEvents;
            var expected = TestEvents.Select(e => e.IsPreferredEvent);
            Assert.Equal(expected.Count(), actual.Count());
        }        

        [Fact]
        public void GetEventByIdTest()
        {
            var mockContext = GetMockContext();
            var repository = new EventRepository(mockContext.Object);
            var expected = mockContext.Object.Events.First();
            var actual = repository.GetEventById(expected.Id);
            Assert.NotNull(actual);
        }

        [Fact]
        public void CreateTest()
        {
            var mockContext = GetMockContext();
            var repository = new EventRepository(mockContext.Object);
            Assert.Equal(TestEvents.Count(), mockContext.Object.Events.Count());
            repository.Create(TestObjects.Event1);
            Assert.Equal(TestEvents.Count(), repository.Events.Count());
        }

        [Fact]
        public void EditTest()
        {
            var mockContext = GetMockContext();
            var repository = new EventRepository(mockContext.Object);
            var item = mockContext.Object.Events.First();
            repository.Edit(item);
            Assert.NotNull(repository.GetEventById(item.Id));
        }

        [Fact]
        public void DeleteTest()
        {
            var mockContext = GetMockContext();
            var repository = new EventRepository(mockContext.Object);
            var item = mockContext.Object.Events.First();
            repository.Delete(item);
            Assert.Null(mockContext.Object.Events.Find(item.Id));
        }

        [Fact]
        public void ExistsTest()
        {
            var mockContext = GetMockContext();
            var repository = new EventRepository(mockContext.Object);
            var exists = repository.Exists(mockContext.Object.Events.First());
            Assert.True(exists);
        }
    }
}
