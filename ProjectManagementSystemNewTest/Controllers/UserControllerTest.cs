

using System.Text.Json;

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
            List<User> expectedUserList = new() { MockUserDetails() };
            //Setup
            usersClass.Setup(x => x.GetAll()).Returns(GetSuccessResult(expectedUserList.AsEnumerable()));
            //Actual Call
            dynamic okObjectResult = (userController.getAll() as OkObjectResult).Value;

            List<User> actualUserList = okObjectResult.Data;

            //Assert
            Assert.IsNotNull(actualUserList);
            Assert.AreEqual(expectedUserList, actualUserList);
            Assert.AreEqual(expectedUserList.FirstOrDefault(), actualUserList.FirstOrDefault());
            Assert.AreEqual(expectedUserList.FirstOrDefault().UserName, actualUserList.FirstOrDefault().UserName);
            Assert.AreEqual(expectedUserList.FirstOrDefault().UserAddress, actualUserList.FirstOrDefault().UserAddress);
            Assert.AreEqual(expectedUserList.FirstOrDefault().UserContact, actualUserList.FirstOrDefault().UserContact);
            Assert.AreEqual(expectedUserList.FirstOrDefault().UserEmail, actualUserList.FirstOrDefault().UserEmail);
            Assert.AreEqual(expectedUserList.FirstOrDefault().UserPassword, actualUserList.FirstOrDefault().UserPassword);
            Assert.AreEqual(expectedUserList.FirstOrDefault().UserId, actualUserList.FirstOrDefault().UserId);
        }

        private static User MockUserDetails()
        {
            return new()
            {
                UserName = "Mock User",
                UserAddress = "Mock User Address",
                UserContact = "123234123",
                UserEmail = "mockUser@gmail.com",
                UserPassword = "mockUserPassword",
                UserId = 1
            };
        }
    }
}
