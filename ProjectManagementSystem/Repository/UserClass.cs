using ProjectManagementSystem.Models;
using MySql.Data.MySqlClient;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using System.Data;
using ProjectManagementSystem.Constants;
using System.Net;
using MySqlX.XDevAPI.Common;

namespace ProjectManagementSystem.Business
{
    public class UserClass : IUserClass
    {
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
        public Result<int> AddUser(User user)
        {
            Asserts.IsNotNull(user, "No Data Fount to insert!.");
            Asserts.IsNotEmpty(user.userEmail, "Email cannot be empty!");
            Asserts.IsNotEmpty(user.userPassword, "Password cannot be empty!");
            Asserts.IsNotEmpty(user.userRole, "Role cannot be empty!");

            Result<int> result = new();
            try
            {
                string query = IQueries.ADD_USER_QUERY;

                string password = GetHashedPassword(user.userPassword);

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add(name: "name", value: user.userName, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "address", value: user.userAddress, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "contact", value: user.userContact, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "password", value: password, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "role", value: user.userRole, direction: ParameterDirection.Input);

                int userResult = (int)unitOfWork.ExecuteQuery<User>(query, dynamicParameters).FirstOrDefault().userId;
                result.Data = userResult;
                result.IsSuccessfull = true;
                result.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                SetUnprocessableEntity(result, IQueries.UNPROCESSABLE_ENTITY, ex.Message);
            }

            //EmailTemplate(user.userEmail, subject, body);

            return result;
        }

        /// <summary>
        /// Updates password of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result<User> UpdatePassword(User user)
        {
            Result<User> result = new();
            try
            {
                var userDetails = GetUserById((int)user.userId);
                string query = IQueries.UPDATE_PASSWORD;
                if (userDetails.Data != null)
                {
                    string password = GetHashedPassword(user.userPassword);

                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add(name: "id", value: user.userId, direction: ParameterDirection.Input);
                    dynamicParameters.Add(name: "address", value: password, direction: ParameterDirection.Input);

                    unitOfWork.ExecuteQuery<User>(query, dynamicParameters);
                    result.IsSuccessfull = true;
                    result.Data = user;
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    SetUnprocessableEntity(result, IQueries.BAD_REQUEST, $"User with id {user.userId} Not exist to update password.");
                }
            }
            catch(Exception ex)
            {
                SetUnprocessableEntity(result, IQueries.UNPROCESSABLE_ENTITY, ex.Message);
            }

            return result;
        }

        private static void SetUnprocessableEntity<T>(Result<T> result, int statusCode, string message)
        {
            result.IsSuccessfull = false;
            result.StatusCode = statusCode;
            result.Message = message;
        }

        /// <summary>
        /// sends notification to user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string SendNotification(User user)
        {
            string subject = IQueries.SUBJECT_FOR_NOTIFICATION;
            string body = user.userAddress;

            EmailTemplate(user.userEmail, subject, body);

            return IQueries.EMAIL_SENT;
        }

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UpdatePasswordByEmail(User user)
        {
            string password = GetHashedPassword(user.userPassword);

            string query = IQueries.UPDATE_PASSWORD_BY_EMAIL;

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: password, direction: ParameterDirection.Input);

            unitOfWork.ExecuteQuery<User>(query, dynamicParameters);

            return user;
        }

        /// <summary>
        /// returns users list depends on email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<User> GetEmail(User user)
        {
            IEnumerable<User> users;
            string query = IQueries.GET_EMAIL;

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);

            users = unitOfWork.ExecuteQuery<User>(query, dynamicParameters);

            return users;
        }

        /// <summary>
        /// returns users list depends on id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result<IEnumerable<User>> GetUserById(int userId)
        {
            Asserts.IsTrue(userId > 0, "User Id is not Valid");

            Result<IEnumerable<User>> result = new();
            try
            {
                string query = IQueries.GET_USER_BY_ID;

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add(name: "id", value: userId, direction: ParameterDirection.Input);

                var userResult = unitOfWork.ExecuteQuery<User>(query, dynamicParameters);
                Asserts.IsNotNull(userResult, $"Unable to get user details for user id {userId}");

                if (userResult.Any())
                {
                    result.Data = userResult;
                    result.IsSuccessfull = true;
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    SetUnprocessableEntity(result, IQueries.BAD_REQUEST, $"User Not found for {userId} user id.");
                }
            }
            catch(Exception ex)
            {
                SetUnprocessableEntity(result, IQueries.BAD_REQUEST, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Returns All users from DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users;
            string query = IQueries.GET_ALL;

            users = unitOfWork.ExecuteQuery<User>(query);
            Asserts.IsNotNull(users, "Unable to get User Details");

            return users;
        }

        /// <summary>
        /// Returns required fields to display on Admins Dashboard.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> AdminDashboard()
        {
            IEnumerable<User> users;
            string query = IQueries.ADMIN_DASHBOARD;

            users = unitOfWork.ExecuteQuery<User>(query);
            Asserts.IsNotNull(users, "Unable to get User Details");

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
            string otp = r.Next(100000, 1000000).ToString();
            Asserts.IsTrue(otp.Length == 6, "Failed To generate OPT");

            string query = IQueries.OTP;

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
            string query = IQueries.CHECK_OTP;

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
        public Result<IEnumerable<User>> GetUserForLogIn(User user)
        {
            Result<IEnumerable<User>> result = new();

            try
            {
                string password = GetHashedPassword(user.userPassword);

                string query = IQueries.GET_USER_FOR_LOGIN;

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "nm", value: password, direction: ParameterDirection.Input);

                var userResult = unitOfWork.ExecuteQuery<User>(query, dynamicParameters);
                if(userResult.Any())
                {
                    result.Data = userResult;
                    result.IsSuccessfull = true;
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    SetUnprocessableEntity(result, IQueries.BAD_REQUEST, "Invalid user name and password!");
                }
            }
            catch (Exception ex)
            {
                SetUnprocessableEntity(result, IQueries.UNPROCESSABLE_ENTITY, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update users details.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User UpdateUser(User user)
        {
            string query = IQueries.UPDATE_USER;

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "name", value: user.userName, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: user.userAddress, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "contact", value: user.userContact, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "role", value: user.userRole, direction: ParameterDirection.Input);

            unitOfWork.ExecuteQuery<User>(query, dynamicParameters);

            return user;
        }

        /// <summary>
        /// Deletes User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public String DeleteUser(int id)
        {
            string query = IQueries.DELETE_USER;
            
            unitOfWork.ExecuteQuery<User>(query);

            return IQueries.DELETED;
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
            MailboxAddress from = new MailboxAddress(IQueries.USER_NAME, IQueries.EMAIL);
            MailboxAddress to = new MailboxAddress(emailId, emailId);
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect(IQueries.EMAIL_SERVER, 587, false);
            client.Authenticate(IQueries.EMAIL, IQueries.KEY);
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
