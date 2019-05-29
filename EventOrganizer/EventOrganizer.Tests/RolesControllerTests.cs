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
    public class RolesControllerTests
    {
        private static readonly IEnumerable<User> TestUsers = new[]
        {
            TestObjects.User1,
            TestObjects.User2,
            TestObjects.User3
        };

        private static readonly List<IdentityRole> TestRoles = new List<IdentityRole>()
        {
            new IdentityRole("User"),
            new IdentityRole("Admin"),
            new IdentityRole("Official")
        };


        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            return userManager;
        }

        private Mock<RoleManager<IdentityRole>> GetRoleManagerMock()
        {
            var roleManager = new Mock<RoleManager<IdentityRole>>(
                    new Mock<IRoleStore<IdentityRole>>().Object,
                    new IRoleValidator<IdentityRole>[0],
                    null, null, null);
            return roleManager;
        }

        [Fact]
        public void IndexModelTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            rmMock.Setup(item => item.Roles).Returns(TestRoles.AsQueryable());
            var result = rolesController.Index();
            
            Assert.NotNull(result);
            Assert.Equal(rolesController.ViewData.Model.ToString(), TestRoles.AsQueryable().ToString());
        }

        [Fact]
        public void CreateTestNotNull()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);

            var result = rolesController.Index();

            Assert.NotNull(result);
        }

        [Fact]
        public void CreateNullOrEmptyNameTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);


            var result = rolesController.Create(null);

            Assert.NotNull(result);
            Assert.Null(rolesController.ViewData.Model);
        }

        [Fact]
        public async void CreateSuccessTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            rmMock.Setup(item => item.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await rolesController.Create("rolename");

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void CreateErrorTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            IdentityError ErrorMessage = new IdentityError { Description = "something was wrong" };
            rmMock.Setup(item => item.CreateAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Failed(ErrorMessage));

            rolesController.ModelState.Clear();
            var result = await rolesController.Create("rolename");

            Assert.NotNull(result);
            Assert.Single(rolesController.ModelState[""].Errors);
            Assert.Equal(rolesController.ModelState[""].Errors[0].ErrorMessage.ToString(), ErrorMessage.Description.ToString());
        }

        [Fact]
        public async void DeleteNullRoleTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            var role = TestRoles.AsQueryable().FirstOrDefault(it => it.Name == "norole");
            rmMock.Setup(item => item.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(role);
            var result = await rolesController.Delete("-1");
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void DeleteExistingRoleTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            var role = TestRoles.AsQueryable().FirstOrDefault(it => it.Name == "User");
            rmMock.Setup(item => item.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(role);
            rmMock.Setup(item => item.DeleteAsync(It.IsAny<IdentityRole>()))
                .ReturnsAsync(IdentityResult.Success);
            var result = await rolesController.Delete("1");
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

    }
}
