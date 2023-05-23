using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IFeedbackClass
    {
        public FeedbackModel feedbackByHOD(FeedbackModel feedback);
        public FeedbackModel feedbackByPIC(FeedbackModel feedback);
        public FeedbackModel feedbackByIG(FeedbackModel feedback);
        public List<FeedbackModel> getFeedbackById(FeedbackModel feedback);
    }
}
