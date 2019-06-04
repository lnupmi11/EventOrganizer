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
    public class UserRepositoryTests
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
        public void AllTest()
        {
            var repository = new UserRepository(GetMockContext().Object);
            var actual = repository.All();
            Assert.Equal(TestUsers.Count(), actual.Count());
        }

        [Fact]
        public void GetTest()
        {
            var repository = new UserRepository(GetMockContext().Object);
            var expected = TestUsers.First();
            var actual = repository.Get(expected.Id);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByUserNameTest()
        {
            var repository = new UserRepository(GetMockContext().Object);
            var expected = TestUsers.Last();
            var actual = repository.GetByUserName(expected.UserName);
            Assert.Equal(expected.UserName, actual.UserName);
        }

        [Fact]
        public void CreateTest()
        {
            var mockContext = GetMockContext();
            var repository = new UserRepository(mockContext.Object);
            Assert.Equal(TestUsers.Count(), mockContext.Object.Users.Count());
            repository.Create(TestObjects.User1);
            Assert.Equal(TestUsers.Count(), repository.All().Count());
        }

        [Fact]
        public void UpdateTest()
        {
            var mockContext = GetMockContext();
            var repository = new UserRepository(mockContext.Object);
            var item = mockContext.Object.Users.First();
            repository.Update(item);
            Assert.NotNull(repository.Get(item.Id));
        }

        [Fact]
        public void DeleteTest()
        {
            var mockContext = GetMockContext();
            var repository = new UserRepository(mockContext.Object);
            var item = mockContext.Object.Users.First();
            repository.Delete(item.Id);
            Assert.Null(mockContext.Object.Users.Find(item.Id));
        }

        [Fact]
        public void DeleteNullUserTest()
        {
            var mockContext = GetMockContext();
            var repository = new UserRepository(mockContext.Object);
            repository.Delete("1");
            Assert.Null(mockContext.Object.Users.Find("1"));
        }

    }
}
