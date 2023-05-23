using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IUserClass
    {
        List<UserModel> GetUserForLogIn(UserModel user);

        /// <summary>
        /// Inserts User records into DB.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserModel addUser(UserModel user);

        /// <summary>
        /// Updates password of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserModel updatePassword(UserModel user);

        /// <summary>
        /// Returns All users from DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> getAll();

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserModel updatePasswordByEmail(UserModel user);

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
        public IEnumerable<UserModel> getEmail(UserModel user);

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
        public IEnumerable<UserModel> getUserById(UserModel user);
        public UserModel updateUser(UserModel user);
        public String deleteUser(int id);

        /// <summary>
        /// Returns required fields to display on Admins Dashboard.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> adminDashboard();

        /// <summary>
        /// sends notification to user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string notification(UserModel user);
    }
}
