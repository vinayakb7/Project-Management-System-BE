namespace ProjectManagementSystem.Models
{
    public class UserModel
    {
        public int? userId { get; set; }
        public string? userName { get; set; }
        public string? userAddress { get; set; }
        public string? userEmail { get; set; }
        public string? userContact { get; set; }
        public string? userPassword { get; set; }
        public string? userRole { get; set; }
    }
    public class Forgot
    {
        public int id { get; set; }
        public string? emailId { get; set; }
        public string? otp { get; set; }
    }
}
