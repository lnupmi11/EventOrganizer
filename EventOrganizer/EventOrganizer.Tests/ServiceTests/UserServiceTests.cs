using EventOrganizer.BLL.Services;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Xunit;

namespace EventOrganizer.Tests.ServiceTests
{
    public class UserServiceTests
    {
        private static readonly IEnumerable<User> TestUsers = new[]
        {
            TestObjects.User1,
            TestObjects.User2,
            TestObjects.User3
        };

        private static Mock<EventOrganizerDbContext> GetMockContext()
        {
            var mockContext = new Mock<EventOrganizerDbContext>();
            mockContext.Setup(item => item.Users).Returns(
                MockProvider.GetMockSet(TestUsers).Object
            );
            return mockContext;
        }

        [Fact]
        public void GetAllTest()
        {
            var list = TestUsers;

            var mockContext = new Mock<EventOrganizerDbContext>();
            mockContext.Setup(item => item.Users).Returns(MockProvider.GetMockSet(list).Object);
            var service = new UserService(mockContext.Object);

            var actual = service.GetAll();

            Assert.Equal(list.Count(), actual.Count());
        }

        [Fact]
        public void GetByIdTest()
        {
            var expected = TestUsers.First();
            var service = new UserService(GetMockContext().Object);

            var actual = service.GetById(expected.Id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public void CreateItemTest()
        {
            var mockContext = new Mock<EventOrganizerDbContext>();
            mockContext.Setup(item => item.Users).Returns(MockProvider.GetMockSet(TestUsers).Object);

            var service = new UserService(GetMockContext().Object);

            var expected = TestObjects.User3;
            service.CreateItem(expected);

            Assert.NotNull(expected);
        }

        [Fact]
        public void UpdateItemTest()
        {
            var mockContext = new Mock<EventOrganizerDbContext>();
            mockContext.Setup(item => item.Users).Returns(MockProvider.GetMockSet(TestUsers).Object);

            var service = new UserService(GetMockContext().Object);

            var expected = TestUsers.First();
            expected.UserName = TestObjects.User3.UserName;
            service.UpdateItem(expected);

            Assert.Equal(expected.UserName, TestObjects.User3.UserName);
        }

        [Fact]
        public void DeleteItemTest()
        {
            var mockContext = GetMockContext();
            var service = new UserService(mockContext.Object);

            var item = mockContext.Object.Users.First();

            Assert.NotNull(service.GetById(item.Id));

            service.DeleteById(item.Id);

            Assert.Null(service.GetById(item.Id));
        }

        [Fact]
        public void GetByUserNameTest()
        {
            var expected = TestUsers.First();
            var service = new UserService(GetMockContext().Object);

            var actual = service.GetByUserName(expected.UserName);

            Assert.NotNull(actual);
            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
