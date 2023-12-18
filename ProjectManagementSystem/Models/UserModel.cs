namespace ProjectManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserAddress { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserContact { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
    }
    public class Forgot
    {
        public int Id { get; set; } 
        public string EmailId { get; set; } = string.Empty;
        public string OTP { get; set; } = string.Empty;
    }
}
