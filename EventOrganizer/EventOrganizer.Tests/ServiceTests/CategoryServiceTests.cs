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
    public class CategoryServiceTests
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
        public void GetAllTest()
        {
            var service = new CategoryService(GetMockContext().Object);
            var actual = service.GetAll();
            Assert.Equal(TestCategories.Count(), actual.Count());
        }

        [Fact]
        public void GetCategoryNameAllTest()
        {
            var service = new CategoryService(GetMockContext().Object);

            var expectedName = "All";

            var actualName = service.GetCategoryName("All");
            Assert.Equal(expectedName, actualName);
        }
        
    }

}
