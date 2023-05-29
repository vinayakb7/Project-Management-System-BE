namespace ProjectManagementSystem.Constants
{
    public static class Queries
    {
        #region Subject Constants
        public static readonly string SUBJECT_FOR_REGISTERED_USER = "Registered user name and password for Project Approval System";
        public static readonly string SUBJECT_FOR_NOTIFICATION = "Notification from Admin of Project Approval System";
        public static readonly string EMAIL_SENT = "Email Sent Successfully!";
        public static readonly string DELETED = "Deleted Successfully";
        public static readonly string USER_NAME = "Vinayak Bilagi";
        public static readonly string EMAIL = "vinayakbilagi7@gmail.com";
        public static readonly string KEY = "dddtiaivtybwqyfj";
        public static readonly string EMAIL_SERVER = "smtp.gmail.com";
        #endregion

        #region User Queries
        public static readonly string ADD_USER_QUERY = "projectmanagementsystem.addUser(?,?,?,?,?,?)";
        public static readonly string UPDATE_PASSWORD = "projectmanagementsystem.updatePassword(?,?)";
        public static readonly string UPDATE_PASSWORD_BY_EMAIL = "projectmanagementsystem.updatePasswordByEmail(?,?)";
        public static readonly string GET_EMAIL = "call projectmanagementsystem.getEmail(?);";
        public static readonly string GET_USER_BY_ID = "call projectmanagementsystem.getUserById(?);";
        public static readonly string GET_ALL = "getAllUser()";
        public static readonly string ADMIN_DASHBOARD = "call adminDashboard()";
        public static readonly string OTP = "projectmanagementsystem.otp(?,?)";
        public static readonly string CHECK_OTP = "projectmanagementsystem.checkOTP(?,?)";
        public static readonly string GET_USER_FOR_LOGIN = "call projectmanagementsystem.getUser(?,?);";
        public static readonly string UPDATE_USER = "call projectmanagementsystem.updateUser(?,?,?,?,?,?)";
        public static readonly string DELETE_USER = "call projectmanagementsystem.deleteUser(?);";
        #endregion
    }
}
