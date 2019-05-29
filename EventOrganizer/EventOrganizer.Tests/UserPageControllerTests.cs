using EventOrganizer.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventOrganizer.Tests
{
    public class UserPageControllerTests
    {
        [Fact]
        public void IndexTestNotNull()
        {
            UserPageController contactController = new UserPageController();

            var result = contactController.Index();
            Assert.NotNull(result);
        }
    }
}
