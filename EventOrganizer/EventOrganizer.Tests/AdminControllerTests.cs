using EventOrganizer.Controllers;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace EventOrganizer.Tests
{
    public class AdminControllerTests
    {
        //UserManager<User> userManager = new UserManager<User>
        //    (
        //    new Mock<IUserStore<User>>().Object,
        //    new Mock<IOptions<IdentityOptions>>().Object,
        //    new Mock<IPasswordHasher<User>>().Object,
        //    new IUserValidator<User>[0],
        //    new IPasswordValidator<User>[0],
        //    new Mock<ILookupNormalizer>().Object,
        //    new Mock<IdentityErrorDescriber>().Object,
        //    new Mock<IServiceProvider>().Object,
        //    new Mock<ILogger<UserManager<User>>>().Object
        //    );
        //
        //[Fact]
        //public void CreateTestNotNull()
        //{
        //    Mock<UserManager<User>> userManager = new Mock<UserManager<User>>();
        //    AdminController adminController = new AdminController(userManager.Object);

        //    CreateModel createModel = new CreateModel();
        //    var actionResultTask = adminController.Create(createModel);
        //    actionResultTask.Wait();

        //    var viewResult = actionResultTask.Result as ViewResult;
        //    Assert.NotNull(viewResult);
        //}
    }
}
