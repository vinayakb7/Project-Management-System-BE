using Moq;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Utility;
using ProjectManagementSystem.Models;
using ProjectManagementSystemNewTest.TestUtility;
using ProjectManagementSystem.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementSystemNewTest.Controllers
{
    /// <summary>
    /// Test class for <see cref="UserController"/>.
    /// </summary>
    [TestClass]
    public class UserControllerTest : ResultUtility
    {
        private UserController? userController;
        private Mock<IUserClass>? usersClass;
        private Mock<IConfiguration>? configuration;

        /// <summary>
        /// Test Setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            usersClass = new Mock<IUserClass>();
            configuration = new Mock<IConfiguration>();

            userController = new UserController(usersClass.Object, configuration.Object);
        }

        /// <summary>
        /// Unit test case for GetAll() users.
        /// </summary>
        [TestMethod]
        public void ShouldSuccessfullyGetUserTest()
        {
            //Setup
            usersClass.Setup(x => x.GetAll()).Returns(MockUserData());
            //Actual Call
            var actualData = userController.getAll() as OkObjectResult;
            //Assert
            Assert.IsNotNull(actualData);
        }

        /// <summary>
        /// Mock Data for User.
        /// </summary>
        /// <returns></returns>
        private static Result<IEnumerable<User>> MockUserData()
        {
            List<User>? users = new() {
                new() {
                    userName = "Mock User",
                    userAddress = "Mock User Address",
                    userContact = "123234123",
                    userEmail = "mockUser@gmail.com",
                    userPassword = "mockUserPassword",
                    userId = 1
                }
            };

            return GetSuccessResult(users.AsEnumerable());
        }

    }
}
