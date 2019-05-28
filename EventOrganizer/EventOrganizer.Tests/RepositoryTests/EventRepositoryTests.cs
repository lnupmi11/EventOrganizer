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
        public void GetEventByIdTest()
        {
            var mockContext = GetMockContext();
            var repository = new EventRepository(mockContext.Object);
            var expected = mockContext.Object.Events.First();
            var actual = repository.GetEventById(expected.Id);
            Assert.NotNull(actual);
        }
    }
}
