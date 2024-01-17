namespace ProjectManagementSystemNewTest.Repository
{
    [TestClass]
    public class UserClassTest : RepositoryTestHelper
    {
        private UserClass userClass;

        [TestInitialize]
        public void SetUp()
        {
            userClass = new(GetUnitOfWork());
        }

        [TestMethod]
        public void ShouldSuccessfullyGetUserData()
        {
            Result<IEnumerable<User>> userData = userClass.GetAll();
            Assert.IsNotNull(userData);
        }

        [TestMethod]
        public void ShouldSuccessfullyCreateUserData()
        {
            User expectedUser = MockUserData();
            expectedUser.UserId = userClass.AddUser(expectedUser).Data;
            User actualUserData = userClass.GetUserById(expectedUser.UserId).Data.FirstOrDefault();
            Assert.IsTrue(expectedUser.UserId > 0);
            Assert.AreEqual(expectedUser.UserName, actualUserData.UserName);
            Assert.AreEqual(expectedUser.UserAddress, actualUserData.UserAddress);
            Assert.AreEqual(expectedUser.UserContact, actualUserData.UserContact);
            Assert.AreEqual(expectedUser.UserEmail, actualUserData.UserEmail);
            Assert.AreEqual(expectedUser.UserPassword, actualUserData.UserPassword);
            Assert.AreEqual(expectedUser.UserId, actualUserData.UserId);
        }

        private static User MockUserData()
        {
            return new()
            {
                UserName = "Mock User",
                UserAddress = "Mock User Address",
                UserContact = "123234123",
                UserEmail = "mockUser@gmail.com",
                UserPassword = "mockUserPassword",
                UserRole = "1"
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            UnitOfWork unitOfWork = GetUnitOfWork();
            string setSafeExecution = "SET SQL_SAFE_UPDATES = 0;";
            string deleteUser = "DELETE FROM PROJECTMANAGEMENTSYSTEM.USERTABLE WHERE USERNAME='Mock User';";
            using (var connection = unitOfWork.GetConnection())
            {
                //unitOfWork.ExecuteQuery<string>(setSafeExecution);
                unitOfWork.ExecuteQuery<User>(deleteUser);
            }
        }
    }
}
