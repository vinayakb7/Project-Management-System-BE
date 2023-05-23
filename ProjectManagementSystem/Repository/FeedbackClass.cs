using MySql.Data.MySqlClient;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public class FeedbackClass : IFeedbackClass
    {
        private readonly IConfiguration _configuration;
        public FeedbackClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public FeedbackModel feedbackByHOD(FeedbackModel feedback)
        {
            string query = "call projectmanagementsystem.hodFeedback(?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@fb", feedback.feedbackByHOD);
                    myCommand.Parameters.AddWithValue("@pid", feedback.projectId);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return feedback;
            }
        }
        public FeedbackModel feedbackByPIC(FeedbackModel feedback)
        {
            string query = "call projectmanagementsystem.picFeedback(?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@fb", feedback.feedbackByPIC);
                    myCommand.Parameters.AddWithValue("@pid", feedback.projectId);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return feedback;
            }
        }
        public FeedbackModel feedbackByIG(FeedbackModel feedback)
        {
            string query = "call projectmanagementsystem.igFeedback(?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@fb", feedback.feedbackByIG);
                    myCommand.Parameters.AddWithValue("@pid", feedback.projectId);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return feedback;
            }
        }
        public List<FeedbackModel> getFeedbackById(FeedbackModel feedback)
        {
            List<FeedbackModel> feedbacks = new List<FeedbackModel>();
            string query = "select * from feedback where projectId = ?";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", feedback.projectId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        FeedbackModel tempFeedback = new FeedbackModel();
                        tempFeedback.feedbackId = Convert.ToInt32(myReader["feedbackId"]);
                        tempFeedback.feedbackByHOD = Convert.ToString(myReader["feedbackByHOD"]);
                        tempFeedback.feedbackByPIC = Convert.ToString(myReader["feedbackByPIC"]);
                        tempFeedback.feedbackByIG = Convert.ToString(myReader["feedbackByIG"]);
                        tempFeedback.projectId = Convert.ToInt32(myReader["projectId"]);
                        feedbacks.Add(tempFeedback);
                    }
                    mycon.Close();
                    return feedbacks;
                }
            }
        }
    }
}
