using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackClass _feedbackClass;
        public FeedbackController(IFeedbackClass feedbackClass)
        {
            _feedbackClass = feedbackClass;
        }
        [HttpPost]
        [Route("HODFeedback")]
        public IActionResult feedbackByHOD(FeedbackModel feedback)
        {
            return Ok(_feedbackClass.feedbackByHOD(feedback));
        }
        [HttpPost]
        [Route("PICFeedback")]
        public IActionResult feedbackByPIC(FeedbackModel feedback)
        {
            return Ok(_feedbackClass.feedbackByPIC(feedback));
        }
        [HttpPost]
        [Route("IGFeedback")]
        public IActionResult feedbackByIG(FeedbackModel feedback)
        {
            return Ok(_feedbackClass.feedbackByIG(feedback));
        }
        [HttpPost]
        [Route("feedbackByProjectId")]
        public IActionResult getFeedbackById(FeedbackModel feedback)
        {
            return Ok(_feedbackClass.getFeedbackById(feedback));
        }
    }
}
