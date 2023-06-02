namespace ProjectManagementSystem.Models
{
    public class UserModel
    {
        public int userId { get; set; }
        public string userName { get; set; } = string.Empty;
        public string userAddress { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public string userContact { get; set; } = string.Empty;
        public string userPassword { get; set; } = string.Empty;
        public string userRole { get; set; } = string.Empty;
    }
    public class Forgot
    {
        public int id { get; set; } 
        public string emailId { get; set; } = string.Empty;
        public string otp { get; set; } = string.Empty;
    }
}
