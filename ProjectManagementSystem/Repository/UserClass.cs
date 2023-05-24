using ProjectManagementSystem.Models;
using MySql.Data.MySqlClient;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using System.Data;

namespace ProjectManagementSystem.Business
{
    public class UserClass : IUserClass
    {
        #region Constant Variables
        private static readonly string SUBJECT_FOR_REGISTERED_USER = "Registered user name and password for Project Approval System";
        private static readonly string SUBJECT_FOR_NOTIFICATION = "Notification from Admin of Project Approval System";
        private static readonly string EMAIL_SENT = "Email Sent Successfully!";
        #endregion

        private static readonly string ADD_USER_QUERY = "projectmanagementsystem.addUser(?,?,?,?,?,?)";

        private readonly IUnitOfWork unitOfWork;
        public UserClass(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Inserts User records into DB.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel AddUser(UserModel user)
        {
            string query = ADD_USER_QUERY;

            string password = GetHashedPassword(user.userPassword);

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "name", value: user.userName, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: user.userAddress, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "contact", value: user.userContact, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "password", value: password, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "role", value: user.userRole, direction: ParameterDirection.Input);

            unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);

            string subject = SUBJECT_FOR_REGISTERED_USER;
            string body = "Hello " + user.userName + ",<br /> Your user name is " + user.userEmail + " and password is " + user.userPassword + ".<br />You can use this details to log in to your accont.<br />You can change it later.<br />Thanks & Regard,<br />Admin";

            EmailTemplate(user.userEmail, subject, body);

            return user;
        }

        /// <summary>
        /// Updates password of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel UpdatePassword(UserModel user)
        {
            string query = "projectmanagementsystem.updatePassword(?,?)";

            string password = GetHashedPassword(user.userPassword);

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userId, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: password, direction: ParameterDirection.Input);

            unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);

            return user;
        }

        /// <summary>
        /// sends notification to user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string SendNotification(UserModel user)
        {
            string subject = SUBJECT_FOR_NOTIFICATION;
            string body = user.userAddress;

            EmailTemplate(user.userEmail, subject, body);

            return EMAIL_SENT;
        }

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel UpdatePasswordByEmail(UserModel user)
        {
            string password = GetHashedPassword(user.userPassword);

            string query = "projectmanagementsystem.updatePasswordByEmail(?,?)";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: password, direction: ParameterDirection.Input);

            unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);

            return user;
        }

        /// <summary>
        /// returns users list depends on email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> GetEmail(UserModel user)
        {
            IEnumerable<UserModel> users;
            string query = "call projectmanagementsystem.getEmail(?);";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);

            users = unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);

            return users;
        }

        /// <summary>
        /// returns users list depends on id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> GetUserById(UserModel user)
        {
            IEnumerable<UserModel> users;
            string query = "call projectmanagementsystem.getUserById(?);";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userId, direction: ParameterDirection.Input);

            users = unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);

            return users;
        }

        /// <summary>
        /// Returns All users from DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> GetAll()
        {
            IEnumerable<UserModel> users;
            string query = "getAllUser()";

            users = unitOfWork.ExecuteQuery<UserModel>(query);

            return users;
        }

        /// <summary>
        /// Returns required fields to display on Admins Dashboard.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> AdminDashboard()
        {
            IEnumerable<UserModel> users;
            string query = "adminDashboard()";

            users = unitOfWork.ExecuteQuery<UserModel>(query);

            return users;
        }

        /// <summary>
        /// Sends OTP to user via email.
        /// </summary>
        /// <param name="forgot"></param>
        /// <returns></returns>
        public Forgot SendOTP(Forgot forgot)
        {
            Random r = new Random();
            var otp = r.Next(100000, 1000000).ToString();
            string query = "projectmanagementsystem.otp(?,?)";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "email", value: forgot.emailId, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "otp", value: otp, direction: ParameterDirection.Input);

            unitOfWork.ExecuteQuery<Forgot>(query, dynamicParameters);

            string subject = "OTP To Change Password";
            string body = "Your OTP to change Password is " + otp + "<br />Thanks & Regards<br/>Admin";

            EmailTemplate(forgot.emailId, subject, body);

            return forgot;
        }

        /// <summary>
        /// Compares OTP.
        /// </summary>
        /// <param name="forgot"></param>
        /// <returns></returns>
        public IEnumerable<Forgot> CheckOTP(Forgot forgot)
        {
            IEnumerable<Forgot> forgotL;
            string query = "projectmanagementsystem.checkOTP(?,?)";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "email", value: forgot.emailId, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "otp", value: forgot.otp, direction: ParameterDirection.Input);

            forgotL = unitOfWork.ExecuteQuery<Forgot>(query, dynamicParameters);

            return forgotL;
        }

        /// <summary>
        /// Get User Model for log in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> GetUserForLogIn(UserModel user)
        {
            IEnumerable<UserModel> users;

            string password = GetHashedPassword(user.userPassword);

            string query = "call projectmanagementsystem.getUser(?,?);";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "nm", value: password, direction: ParameterDirection.Input);

            users = unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);

            return users;
        }

        /// <summary>
        /// Update users details.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel UpdateUser(UserModel user)
        {
            string query = "call projectmanagementsystem.updateUser(?,?,?,?,?,?)";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "name", value: user.userName, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: user.userAddress, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "contact", value: user.userContact, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "role", value: user.userRole, direction: ParameterDirection.Input);

            unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);

            return user;
        }

        /// <summary>
        /// Deletes User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public String DeleteUser(int id)
        {
            string query = "call projectmanagementsystem.deleteUser(?);";
            
            unitOfWork.ExecuteQuery<UserModel>(query);

            return ("Deleted Successfully");
        }

        /// <summary>
        /// Password converts to hash.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string GetHashedPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        /// <summary>
        /// SMTP code to send OTP.
        /// </summary>
        /// <param name="forgot"></param>
        /// <param name="otp"></param>
        private static void EmailTemplate(string emailId, string subject, string body)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Vinayak Bilagi", "vinayakbilagi7@gmail.com");
            MailboxAddress to = new MailboxAddress(emailId, emailId);
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("vinayakbilagi7@gmail.com", "dddtiaivtybwqyfj");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
