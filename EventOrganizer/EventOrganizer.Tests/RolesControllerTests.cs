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
        private static readonly List<string> RolesList = new List<string>()
        {
            "User",
            "Admin",
            "Official"
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

        [Fact]
        public void CreateViewTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);

            var result = rolesController.Create();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
        }

        [Fact]
        public void UserListViewTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            umMock.Setup(item => item.Users)
                .Returns(TestUsers.AsQueryable());
            var result = rolesController.UserList();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
            var expected = TestUsers.ToList<User>().ToString();
            var actual = rolesController.ViewData.Model.ToString();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void EditNullUserTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            User usr = null;
            umMock.Setup(item => item.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(usr);

            var result = await rolesController.Edit("invaliId");
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            umMock.Setup(item => item.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(TestObjects.User1);
            umMock.Setup(item => item.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(RolesList);
            rmMock.Setup(item => item.Roles)
                .Returns(TestRoles.AsQueryable());
            ChangeRoleViewModel mdl = new ChangeRoleViewModel();

            var result = await rolesController.Edit("1");

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ViewResult>(result);
            var expected = mdl.ToString();
            var actual = rolesController.ViewData.Model.ToString();
            Assert.Equal(expected, actual);

        }
       
        [Fact]
        public async void EditPostNullUserTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            User usr = null;
            umMock.Setup(item => item.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(usr);
            
            var result = await rolesController.Edit("invaliId", RolesList);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditPostTest()
        {
            var umMock = GetUserManagerMock();
            var rmMock = GetRoleManagerMock();

            var rolesController = new RolesController(rmMock.Object, umMock.Object);
            umMock.Setup(item => item.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(TestObjects.User1);
            umMock.Setup(item => item.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(RolesList);
            rmMock.Setup(item => item.Roles)
                .Returns(TestRoles.AsQueryable());
            
            var result = await rolesController.Edit("invaliId", RolesList);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
    }
}
