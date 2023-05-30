using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IUserClass
    {

        // <summary>
        /// Get User Model for log in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Result<IEnumerable<UserModel>> GetUserForLogIn(UserModel user);

        /// <summary>
        /// Inserts User records into DB.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Result<int> AddUser(UserModel user);

        /// <summary>
        /// Updates password of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Result<UserModel> UpdatePassword(UserModel user);

        /// <summary>
        /// Returns All users from DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> GetAll();

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserModel UpdatePasswordByEmail(UserModel user);

        /// <summary>
        /// Sends OTP to user via email.
        /// </summary>
        /// <param name="forgot"></param>
        /// <returns></returns>
        public Forgot SendOTP(Forgot forgot);

        /// <summary>
        /// returns users list depends on email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> GetEmail(UserModel user);

        /// <summary>
        /// Compares OTP.
        /// </summary>
        /// <param name="forgot"></param>
        /// <returns></returns>
        public IEnumerable<Forgot> CheckOTP(Forgot forgot);

        /// <summary>
        /// returns users list depends on id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result<IEnumerable<UserModel>> GetUserById(int userId);

        /// <summary>
        /// Update users details.
        /// </summary>
        /// <param name="user"></param>
        public UserModel UpdateUser(UserModel user);

        /// <summary>
        /// Deletes User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public String DeleteUser(int id);

        /// <summary>
        /// Returns required fields to display on Admins Dashboard.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> AdminDashboard();

        /// <summary>
        /// sends notification to user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string SendNotification(UserModel user);
    }
}
