using ProjectManagementSystem.Models;
using MySql.Data.MySqlClient;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Asn1.Cms;
using Dapper;
using System.Data;

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
        public UserModel addUser(UserModel user)
        {
            string query = "projectmanagementsystem.addUser(?,?,?,?,?,?)";
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(user.userPassword);
            var hashedPassword = sha.ComputeHash(asByteArray);
            var password = Convert.ToBase64String(hashedPassword);

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "name", value: user.userName, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: user.userAddress, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "contact", value: user.userContact, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "password", value: password, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "role", value: user.userRole, direction: ParameterDirection.Input);

            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {
                unitOfWork.Query<UserModel>(query, dynamicParameters, null, commandType: CommandType.Text);
            }
            //SendEmail(user);
            return user;
        }

        /// <summary>
        /// Sends email to given email address.
        /// </summary>
        /// <param name="user"></param>
        private static void SendEmail(UserModel user)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Vinayak Bilagi", "vinayakbilagi7@gmail.com");
            MailboxAddress to = new MailboxAddress(user.userName, user.userEmail);
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = "Registered user name and password for Project Approval System";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "Hello " + user.userName + ",<br /> Your user name is " + user.userEmail + " and password is " + user.userPassword + ".<br />You can use this details to log in to your accont.<br />You can change it later.<br />Thanks & Regard,<br />Admin";
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("vinayakbilagi7@gmail.com", "dddtiaivtybwqyfj");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }

        /// <summary>
        /// Updates password of user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel updatePassword(UserModel user)
        {
            string query = "projectmanagementsystem.updatePassword(?,?)";
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(user.userPassword);
            var hashedPassword = sha.ComputeHash(asByteArray);
            var password = Convert.ToBase64String(hashedPassword);

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userId, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: password, direction: ParameterDirection.Input);

            using (var mycon = unitOfWork.GetConnection())
            {
                unitOfWork.Query<UserModel>(query, dynamicParameters, null, commandType: CommandType.Text);
            }
            return user;
        }

        /// <summary>
        /// sends notification to user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string notification(UserModel user)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Vinayak Bilagi", "vinayakbilagi7@gmail.com");
            MailboxAddress to = new MailboxAddress(user.userEmail, user.userEmail);
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = "Notification from Admin of Project Approval System";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = user.userAddress;
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("vinayakbilagi7@gmail.com", "dddtiaivtybwqyfj");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            return "Email Sent Successfully";
        }

        /// <summary>
        /// Updates user's password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel updatePasswordByEmail(UserModel user)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(user.userPassword);
            var hashedPassword = sha.ComputeHash(asByteArray);
            var password = Convert.ToBase64String(hashedPassword);
            string query = "projectmanagementsystem.updatePasswordByEmail(?,?)";
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "email", value: user.userEmail, direction: ParameterDirection.Input);
            dynamicParameters.Add(name: "address", value: password, direction: ParameterDirection.Input);

            using (var mycon = unitOfWork.GetConnection())
            {
                unitOfWork.Query<UserModel>(query, dynamicParameters, null, commandType: CommandType.Text);
            }
            return user;
        }

        /// <summary>
        /// returns users list depends on email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> getEmail(UserModel user)
        {
            IEnumerable<UserModel> users;
            string query = "call projectmanagementsystem.getEmail(?);";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userEmail, direction: ParameterDirection.Input);

            using (var mycon = unitOfWork.GetConnection())
            {
                users = unitOfWork.Query<UserModel>(query, dynamicParameters, null, commandType: CommandType.Text);
            }

            return users;
        }

        /// <summary>
        /// returns users list depends on id.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<UserModel> getUserById(UserModel user)
        {
            IEnumerable<UserModel> users;
            string query = "call projectmanagementsystem.getUserById(?);";

            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add(name: "id", value: user.userId, direction: ParameterDirection.Input);

            using (var mycon = unitOfWork.GetConnection())
            {
                users = unitOfWork.Query<UserModel>(query, dynamicParameters, null, commandType: CommandType.Text);
            }

            return users;
        }
        public List<UserModel> getAll()
        {
            List<UserModel> users = new List<UserModel>();
            string query = "getAllUser()";
            
            MySqlDataReader myReader;
            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        UserModel tempuser = new UserModel();
                        tempuser.userId = Convert.ToInt32(myReader["userId"]);
                        tempuser.userName = Convert.ToString(myReader["userName"]);
                        tempuser.userAddress = Convert.ToString(myReader["userAddress"]);
                        tempuser.userContact = Convert.ToString(myReader["userContact"]);
                        tempuser.userEmail = Convert.ToString(myReader["userEmail"]);
                        tempuser.userPassword = Convert.ToString(myReader["userPassword"]);
                        tempuser.userRole = Convert.ToString(myReader["role"]);
                        users.Add(tempuser);
                    }
                    mycon.Close();
                    return users;
                }
            }
        }

        public List<UserModel> adminDashboard()
        {
            List<UserModel> users = new List<UserModel>();
            string query = "adminDashboard()";
            
            MySqlDataReader myReader;
            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        UserModel tempuser = new UserModel();
                        tempuser.userName = Convert.ToString(myReader["Total_Users"]);
                        tempuser.userAddress = Convert.ToString(myReader["Total_Student"]);
                        tempuser.userContact = Convert.ToString(myReader["Total_HOD"]);
                        tempuser.userEmail = Convert.ToString(myReader["Total_PIC"]);
                        tempuser.userPassword = Convert.ToString(myReader["Total_IG"]);
                        tempuser.userRole = Convert.ToString(myReader["Total_Project"]);
                        users.Add(tempuser);
                    }
                    mycon.Close();
                    return users;
                }
            }
        }
        public Forgot gotPassword(Forgot forgot)
        {
            Random r = new Random();
            var otp = r.Next(100000, 1000000).ToString();
            string query = "projectmanagementsystem.otp(?,?)";
            
            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@email", forgot.emailId);
                    myCommand.Parameters.AddWithValue("@otp", otp);
                    myCommand.ExecuteReader();
                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("Vinayak Bilagi", "vinayakbilagi7@gmail.com");
                    MailboxAddress to = new MailboxAddress(forgot.emailId, forgot.emailId);
                    message.From.Add(from);
                    message.To.Add(to);
                    message.Subject = "OTP To Change Password";
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = "Your OTP to change Password is "+otp+"<br />Thanks & Regards<br/>Admin";
                    message.Body = bodyBuilder.ToMessageBody();
                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("vinayakbilagi7@gmail.com", "dddtiaivtybwqyfj");
                    client.Send(message);
                    client.Disconnect(true);
                    client.Dispose();
                    mycon.Close();
                }
                return forgot;
            }
        }
        public List<Forgot> checkOTP(Forgot forgot)
        {
            List<Forgot> forgotL = new List<Forgot>();
            string query = "projectmanagementsystem.checkOTP(?,?)";
            
            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {
                MySqlDataReader myReader;
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@email", forgot.emailId);
                    myCommand.Parameters.AddWithValue("@otp", forgot.otp);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        Forgot forgotPass = new Forgot();
                        forgotPass.id = Convert.ToInt32(myReader["optId"]);
                        forgotPass.emailId = Convert.ToString(myReader["emailId"]);
                        forgotPass.otp = Convert.ToString(myReader["otp"]);
                        forgotL.Add(forgotPass);
                    }
                    mycon.Close();
                    return forgotL;
                }
            }
        }

        public List<UserModel> getUser(UserModel user)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(user.userPassword);
            var hashedPassword = sha.ComputeHash(asByteArray);
            var password = Convert.ToBase64String(hashedPassword);
            List<UserModel> users = new List<UserModel>();   
            string query = "call projectmanagementsystem.getUser(?,?);";
            
            MySqlDataReader myReader;
            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", user.userEmail);
                    myCommand.Parameters.AddWithValue("@nm", password);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        UserModel tempuser = new UserModel();
                        tempuser.userId = Convert.ToInt32(myReader["userId"]);
                        tempuser.userName = Convert.ToString(myReader["userName"]);
                        tempuser.userAddress = Convert.ToString(myReader["userAddress"]);
                        tempuser.userContact = Convert.ToString(myReader["userContact"]);
                        tempuser.userEmail = Convert.ToString(myReader["userEmail"]);
                        tempuser.userPassword = Convert.ToString(myReader["userPassword"]);
                        tempuser.userRole = Convert.ToString(myReader["userRole"]);
                        users.Add(tempuser);
                    }
                    mycon.Close();
                    return users;
                }
            }
        }
        public UserModel updateUser(UserModel user)
        {
            string query = "call projectmanagementsystem.updateUser(?,?,?,?,?,?)";
            
            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", user.userId);
                    myCommand.Parameters.AddWithValue("@name", user.userName);
                    myCommand.Parameters.AddWithValue("@address", user.userAddress);
                    myCommand.Parameters.AddWithValue("@contact", user.userContact);
                    myCommand.Parameters.AddWithValue("@email", user.userEmail);
                    myCommand.Parameters.AddWithValue("@role", user.userRole);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return user;
            }
        }

        public String deleteUser(int id)
        {
            string query = "call projectmanagementsystem.deleteUser(?);";
            
            MySqlDataReader myReader;
            using (MySqlConnection mycon = unitOfWork.GetConnection())
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    mycon.Close();
                }
            }
            return ("Deleted Successfully");
        }
    }
}
