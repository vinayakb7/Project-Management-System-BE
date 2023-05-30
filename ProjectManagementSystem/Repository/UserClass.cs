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
        public Result<int> AddUser(UserModel user)
        {
            Result<int> result = new();
            try
            {
                string query = Queries.ADD_USER_QUERY;

                string password = GetHashedPassword(user.userPassword);

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add(name: "name", value: user.userName, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "address", value: user.userAddress, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "contact", value: user.userContact, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "password", value: password, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "role", value: user.userRole, direction: ParameterDirection.Input);

                int userResult = (int)unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters).FirstOrDefault().userId;
                result.Data = userResult;
                result.IsSuccessfull = true;
                result.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                SetUnprocessableEntity(result, Queries.UNPROCESSABLE_ENTITY, ex.Message);
            }

            //EmailTemplate(user.userEmail, subject, body);

            return result;
        }

        /// <summary>
        /// Updates password of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result<UserModel> UpdatePassword(UserModel user)
        {
            Result<UserModel> result = new();
            try
            {
                var userDetails = GetUserById((int)user.userId);
                string query = Queries.UPDATE_PASSWORD;
                if (userDetails.Data != null)
                {
                    string password = GetHashedPassword(user.userPassword);

                    DynamicParameters dynamicParameters = new();
                    dynamicParameters.Add(name: "id", value: user.userId, direction: ParameterDirection.Input);
                    dynamicParameters.Add(name: "address", value: password, direction: ParameterDirection.Input);

                    unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);
                    result.IsSuccessfull = true;
                    result.Data = user;
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    SetUnprocessableEntity(result, Queries.BAD_REQUEST, $"User with id {user.userId} Not exist to update password.");
                }
            }
            catch(Exception ex)
            {
                SetUnprocessableEntity(result, Queries.UNPROCESSABLE_ENTITY, ex.Message);
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
        public string SendNotification(UserModel user)
        {
            string subject = Queries.SUBJECT_FOR_NOTIFICATION;
            string body = user.userAddress;

            EmailTemplate(user.userEmail, subject, body);

            return Queries.EMAIL_SENT;
        }

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel UpdatePasswordByEmail(UserModel user)
        {
            string password = GetHashedPassword(user.userPassword);

            string query = Queries.UPDATE_PASSWORD_BY_EMAIL;

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
            string query = Queries.GET_EMAIL;

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
        public Result<IEnumerable<UserModel>> GetUserById(int userId)
        {
            Result<IEnumerable<UserModel>> result = new();
            try
            {
                string query = Queries.GET_USER_BY_ID;

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add(name: "id", value: userId, direction: ParameterDirection.Input);

                var userResult = unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);
                if (userResult.Any())
                {
                    result.Data = userResult;
                    result.IsSuccessfull = true;
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    SetUnprocessableEntity(result, Queries.BAD_REQUEST, $"User Not found for {userId} user id.");
                }
            }
            catch(Exception ex)
            {
                SetUnprocessableEntity(result, Queries.BAD_REQUEST, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Returns All users from DB.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserModel> GetAll()
        {
            IEnumerable<UserModel> users;
            string query = Queries.GET_ALL;

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
            string query = Queries.ADMIN_DASHBOARD;

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
            string query = Queries.OTP;

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
            string query = Queries.CHECK_OTP;

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
        public Result<IEnumerable<UserModel>> GetUserForLogIn(UserModel user)
        {
            Result<IEnumerable<UserModel>> result = new();

            try
            {
                string password = GetHashedPassword(user.userPassword);

                string query = Queries.GET_USER_FOR_LOGIN;

                DynamicParameters dynamicParameters = new();
                dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);
                dynamicParameters.Add(name: "nm", value: password, direction: ParameterDirection.Input);

                var userResult = unitOfWork.ExecuteQuery<UserModel>(query, dynamicParameters);
                if(userResult.Any())
                {
                    result.Data = userResult;
                    result.IsSuccessfull = true;
                    result.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    SetUnprocessableEntity(result, Queries.BAD_REQUEST, "Invalid user name and password!");
                }
            }
            catch (Exception ex)
            {
                SetUnprocessableEntity(result, Queries.UNPROCESSABLE_ENTITY, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update users details.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel UpdateUser(UserModel user)
        {
            string query = Queries.UPDATE_USER;

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
            string query = Queries.DELETE_USER;
            
            unitOfWork.ExecuteQuery<UserModel>(query);

            return Queries.DELETED;
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
            MailboxAddress from = new MailboxAddress(Queries.USER_NAME, Queries.EMAIL);
            MailboxAddress to = new MailboxAddress(emailId, emailId);
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = subject;
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect(Queries.EMAIL_SERVER, 587, false);
            client.Authenticate(Queries.EMAIL, Queries.KEY);
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
