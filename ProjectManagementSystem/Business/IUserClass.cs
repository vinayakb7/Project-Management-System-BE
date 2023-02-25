using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IUserClass
    {
        List<UserModel> getUser(UserModel user);
        UserModel addUser(UserModel user);
        UserModel updatePassword(UserModel user);
        public List<UserModel> getAll();
        UserModel updatePasswordByEmail(UserModel user);
        public Forgot gotPassword(Forgot forgot);
        public List<UserModel> getEmail(UserModel user);
        public List<Forgot> checkOTP(Forgot forgot);
        public List<UserModel> getUserById(UserModel user);
        public UserModel updateUser(UserModel user);
        public String deleteUser(int id);
        public List<UserModel> adminDashboard();
        public string notification(UserModel user);
    }
}
