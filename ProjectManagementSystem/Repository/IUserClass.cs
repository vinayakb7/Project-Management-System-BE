using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IUserClass
    {
        List<UserModel> getUser(UserModel user);

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
        public List<UserModel> getAll();

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserModel updatePasswordByEmail(UserModel user);
        public Forgot gotPassword(Forgot forgot);

        /// <summary>
        /// returns users list depends on email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> getEmail(UserModel user);
        public List<Forgot> checkOTP(Forgot forgot);

        /// <summary>
        /// returns users list depends on id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> getUserById(UserModel user);
        public UserModel updateUser(UserModel user);
        public String deleteUser(int id);
        public List<UserModel> adminDashboard();

        /// <summary>
        /// sends notification to user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string notification(UserModel user);
    }
}
