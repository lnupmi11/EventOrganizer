using EventOrganizer.Controllers;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventOrganizer.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void IndexTest()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            AdminController adminController = new AdminController(userManager.Object);
            var listOfUsers = new List<User>() { TestObjects.User1, TestObjects.User2, TestObjects.User3 };

            userManager.Setup(item => item.Users).Returns(
                listOfUsers.AsQueryable()
            );


            var result = adminController.Index();
            var expected = new List<User>() {
                TestObjects.User1, TestObjects.User2, TestObjects.User3
            };

            Assert.NotNull(result);
            Assert.Equal(result.Model.ToString(), expected.ToString());
        }

        [Fact]
        public void CreateTest()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            AdminController adminController = new AdminController(userManager.Object);
            var listOfUsers = new List<User>() { TestObjects.User1, TestObjects.User2, TestObjects.User3 };

            userManager.Setup(item => item.Users).Returns(
                listOfUsers.AsQueryable()
            );

            var result = adminController.Index();

            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateInvalidPasswordTest()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            AdminController adminController = new AdminController(userManager.Object);
            var usr = new User()
            {
                UserName = "Nelya",
                Email = "nelya@gmail.com"
            };

            IdentityError ErrorMessage = new IdentityError { Description = "invalid password" };
            userManager.Setup(item => item.CreateAsync(It.IsAny<User>(), "badpassword"))
                .ReturnsAsync(IdentityResult.Failed(ErrorMessage));

            adminController.ModelState.Clear();
            var result = await adminController.Create(new UserViewModel()
            {
                Name = usr.UserName,
                Email = usr.Email,
                Password = "badpassword"
            });
            Assert.NotNull(result);
            Assert.Single(adminController.ModelState[""].Errors);
            Assert.Equal(adminController.ModelState[""].Errors[0].ErrorMessage.ToString(), ErrorMessage.Description.ToString() );

        }
        [Fact]
        public async void CreateCorrectUserTest()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            AdminController adminController = new AdminController(userManager.Object);
            var usr = new User()
            {
                UserName = "Nelya",
                Email = "nelya@gmail.com"
            };

            userManager.Setup(item => item.CreateAsync(It.IsAny<User>(), "Aa1&aaaaaaaa"))
                .ReturnsAsync(IdentityResult.Success);

            var result = await adminController.Create(new UserViewModel()
            {
                Name = usr.UserName,
                Email = usr.Email,
                Password = "Aa1&aaaaaaaa"
            });
            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);

        }

        [Fact]
        public void CreateViewTest()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            AdminController adminController = new AdminController(userManager.Object);

            var result = adminController.Create();
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public async void InvalidModelStateTest()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            AdminController adminController = new AdminController(userManager.Object);
            adminController.ModelState.AddModelError("invalid", "Invalid model state");

            var result = await adminController.Create(new UserViewModel());

            Assert.NotNull(result);
            Assert.False(adminController.ModelState.IsValid);
            Assert.IsAssignableFrom<ViewResult>(result);
        }

    }
}
