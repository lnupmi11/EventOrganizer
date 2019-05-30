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
    public class CategoryRepositoryTests
    {
        private static readonly IEnumerable<Category> TestCategories = new[]
        {
            TestObjects.Category1,
            TestObjects.Category2,
            TestObjects.Category3,
            TestObjects.Category4,
            TestObjects.Category5
        };

        private static Mock<EventOrganizerDbContext> GetMockContext()
        {
            var mockContext = new Mock<EventOrganizerDbContext>();
            mockContext.Setup(item => item.Categories).Returns(
                MockProvider.GetMockSet(TestCategories).Object
            );
            return mockContext;
        }

        [Fact]
        public void GetCategoriesTest()
        {
            var repository = new CategoryRepository(GetMockContext().Object);
            var actual = repository.Categories;
            Assert.Equal(TestCategories.Count(), actual.Count());
        }
    }
}
