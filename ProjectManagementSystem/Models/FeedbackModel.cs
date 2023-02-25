namespace ProjectManagementSystem.Models
{
    public class FeedbackModel
    {
        public int feedbackId { get; set; }
        public string? feedbackByHOD { get; set; }
        public string? feedbackByPIC { get; set; }
        public string? feedbackByIG { get; set; }
        public int projectId { get; set; }
    }
}
