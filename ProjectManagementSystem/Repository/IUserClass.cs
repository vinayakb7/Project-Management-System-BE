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
        Result<IEnumerable<User>> GetUserForLogIn(User user);

        /// <summary>
        /// Inserts User records into DB.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Result<int> AddUser(User user);

        /// <summary>
        /// Updates password of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Result<User> UpdatePassword(User user);

        /// <summary>
        /// Returns All users from DB.
        /// </summary>
        /// <returns></returns>
        public Result<IEnumerable<User>> GetAll();

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User UpdatePasswordByEmail(User user);

        /// <summary>
        /// Sends OTP to user via email.
        /// </summary>
        /// <param name="forgot"></param>
        /// <returns></returns>
        public Result<string> SendOTP(Forgot forgot);

        /// <summary>
        /// returns users list depends on email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<User> GetEmail(User user);

        /// <summary>
        /// Compares OTP.
        /// </summary>
        /// <param name="forgot"></param>
        /// <returns></returns>
        public Result<IEnumerable<Forgot>> CheckOTP(Forgot forgot);

        /// <summary>
        /// returns users list depends on id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result<IEnumerable<User>> GetUserById(int userId);

        /// <summary>
        /// Update users details.
        /// </summary>
        /// <param name="user"></param>
        public User UpdateUser(User user);

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
        public IEnumerable<User> AdminDashboard();

        /// <summary>
        /// sends notification to user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string SendNotification(User user);
    }
}
