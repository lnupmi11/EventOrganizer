using EventOrganizer.BLL.Interfaces;
using EventOrganizer.Controllers;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using EventOrganizer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EventOrganizer.Tests
{
    public class AccountControllerTests
    {
        private static readonly IEnumerable<User> TestUsers = new[]
        {
            TestObjects.User1,
            TestObjects.User2,
            TestObjects.User3
        };
        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userStore = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            return userManager;
        }

        private Mock<SignInManager<User>> GetSignInManagerMock()
        {
            var context = new Mock<HttpContext>();
            var manager = GetUserManagerMock();
            return new Mock<SignInManager<User>>(manager.Object,
                new HttpContextAccessor { HttpContext = context.Object },
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                null, null, null)
            { CallBase = true };
        }

        private Mock<IUserService> GetUserServiceMock()
        {
            var service = new Mock<IUserService>();
            return service;
        }

        [Fact]
        public void LoginModelTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);

            var result = accountController.Login("returnurl");

            var expected = new LoginViewModel() { ReturnUrl = "returnurl" };
            var actual = accountController.ViewData.Model;
            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public async void LoginNullUserTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);

            var result = await accountController.Login(new LoginViewModel());
            var usr = TestUsers.FirstOrDefault(u => u.UserName == "noname");
            usMock.Setup(item => item.GetByUserName("noname"))
                .Returns(usr);
            var errorlist = Assert.Single(accountController.ModelState[""].Errors);
            var actualMsg = errorlist.ErrorMessage;

            var expectedMsg = "Username/Password not found";

            Assert.Equal(actualMsg, expectedMsg);
            Assert.NotNull(result);
        }
        
        [Fact]
        public async void LoginSuccessRedirectUrlTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            simMock.Setup(item => item.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            simMock.Setup(item => item.SignOutAsync())
                .Returns(Task.FromResult("Success"));
            var usr = TestUsers.FirstOrDefault(u => u.UserName == "username#1");
            usMock.Setup(item => item.GetByUserName("username#1"))
                .Returns(usr);

            var result = await accountController.Login(new LoginViewModel() { UserName = "username#1",ReturnUrl = "redirecturl"});
            Assert.IsAssignableFrom<RedirectResult>(result);
        }

        [Fact]
        public async void LoginSuccessRedirectToListTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            simMock.Setup(item => item.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            simMock.Setup(item => item.SignOutAsync())
                .Returns(Task.FromResult("Success"));
            var usr = TestObjects.User1;
            usMock.Setup(item => item.GetByUserName("username#1"))
                .Returns(usr);

            var result = await accountController.Login(new LoginViewModel() { UserName = "username#1"});
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void LoginFailedToLoginTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            
            simMock.Setup(item => item.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
            simMock.Setup(item => item.SignOutAsync())
                .Returns(Task.FromResult("Success"));
            var usr = TestObjects.User1;
            usMock.Setup(item => item.GetByUserName("username#1"))
                .Returns(usr);

            var result = await accountController.Login(new LoginViewModel() { UserName = "username#1" });
            Assert.IsAssignableFrom<ViewResult>(result);
            var errorlist = Assert.Single(accountController.ModelState[""].Errors);
            var actualMsg = errorlist.ErrorMessage;

            var expectedMsg = "Username/Password not found";

            Assert.Equal(actualMsg, expectedMsg);

        }

        [Fact]
        public void RegisterTestNotNull()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            var result = accountController.Register();

            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteNullUserTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);

            var usr = TestUsers.FirstOrDefault(u => u.UserName == "noname");
            usMock.Setup(item => item.GetById(It.IsAny<string>()))
                .Returns(usr);
            var result = accountController.Delete("-1");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteUserTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);

            var usr = TestObjects.User1;
            usMock.Setup(item => item.GetById(It.IsAny<string>()))
                .Returns(usr);
            var result = accountController.Delete("1");

            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }
        [Fact]
        public async void LogoutTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            simMock.Setup(item => item.SignOutAsync())
                .Returns(Task.FromResult("Success"));
            var result = await accountController.Logout();
            
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void RegisterFailedTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            IdentityError ErrorMessage = new IdentityError { Description = "invalid error" };
            umMock.Setup(item=>item.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(ErrorMessage));

            var loginViewModel = new LoginViewModel()
            {
                UserName = "noname",
                Email = "noemail",
                Password = "Password",
                ReturnUrl = "returnurl"
            };
            var result = await accountController.Register(loginViewModel);

            Assert.NotNull(result);
            Assert.Equal(accountController.ViewData.Model.ToString(), loginViewModel.ToString());
        }

        [Fact]
        public async void RegisterSuccessLoginSuccessWithoutRedirectTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            IdentityError ErrorMessage = new IdentityError { Description = "invalid error" };
            umMock.Setup(item => item.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            umMock.Setup(item => item.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);
            simMock.Setup(item => item.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
           
            var loginViewModel = new LoginViewModel()
            {
                UserName = "noname",
                Email = "noemail",
                Password = "Password",
                ReturnUrl = "returnurl"
            };
            var result = await accountController.Register(loginViewModel);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectResult>(result);
        }

        [Fact]
        public async void RegisterSuccessLoginSuccessWithRedirectTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            IdentityError ErrorMessage = new IdentityError { Description = "invalid error" };
            umMock.Setup(item => item.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            umMock.Setup(item => item.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);
            simMock.Setup(item => item.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            var loginViewModel = new LoginViewModel()
            {
                UserName = "noname",
                Email = "noemail",
                Password = "Password"
            };
            var result = await accountController.Register(loginViewModel);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void RegisterSuccessLoginFailedTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            IdentityError ErrorMessage = new IdentityError { Description = "invalid error" };
            umMock.Setup(item => item.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            umMock.Setup(item => item.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(IdentityResult.Success);
            simMock.Setup(item => item.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            var loginViewModel = new LoginViewModel()
            {
                UserName = "noname",
                Email = "noemail",
                Password = "incorrectPassword"
            };
            var result = await accountController.Register(loginViewModel);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public async void InvalidModelStateTest()
        {
            var umMock = GetUserManagerMock();
            var simMock = GetSignInManagerMock();
            var usMock = GetUserServiceMock();

            var accountController = new AccountController(umMock.Object, simMock.Object, usMock.Object);
            accountController.ModelState.AddModelError("invalid", "Invalid model state");
            var result = await accountController.Login(new LoginViewModel());
            Assert.NotNull(result);
            Assert.False(accountController.ModelState.IsValid);
            



        }
    }
}
