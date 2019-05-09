﻿using EventOrganizer.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventOrganizer.Tests
{
    public class ContactControllerTests
    {
        [Fact]
        public void IndexTestNotNull()
        {
            ContactController contactController = new ContactController();

            var result = contactController.Index();
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexTestModelNameTrue()
        {
            ContactController contactController = new ContactController();

            var result = contactController.Index();
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }
    }
}
