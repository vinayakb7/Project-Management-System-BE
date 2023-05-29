using MailKit.Net.Smtp;
using MimeKit;
using MySql.Data.MySqlClient;
using ProjectManagementSystem.Models;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;
using static System.Net.WebRequestMethods;

namespace ProjectManagementSystem.Business
{
    public class ProjectClass : IProjectClass
    {
        private readonly IConfiguration _configuration;
        public ProjectClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ProjectModel addProject(ProjectModel project)
        {
            string query = "call projectmanagementsystem.addProject(?,?,?,?,?,?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    
                    myCommand.Parameters.AddWithValue("@nm", project.projectName);
                    myCommand.Parameters.AddWithValue("@pd", project.projectDetails);
                    myCommand.Parameters.AddWithValue("@pf", project.projectData);
                    myCommand.Parameters.AddWithValue("@pui",project.userId);
                    myCommand.Parameters.AddWithValue("@hod", project.HODID);
                    myCommand.Parameters.AddWithValue("@pic", project.PICID);
                    myCommand.Parameters.AddWithValue("@pig", project.IGID);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return project;
            }
        }

        public List<ProjectModel> getProject()
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.getProject();";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectId"]);
                        tempProject.projectName = Convert.ToString(myReader["projectName"]);
                        tempProject.projectDetails = Convert.ToString(myReader["projectDetails"]);
                        tempProject.projectData = Convert.ToString(myReader["projectData"]);
                        tempProject.projectStatus = Convert.ToString(myReader["projectStatus"]);
                        tempProject.userId = Convert.ToString(myReader["useId"]);
                        tempProject.HODID = Convert.ToString(myReader["HODID"]);
                        tempProject.PICID = Convert.ToString(myReader["PICID"]);
                        tempProject.IGID = Convert.ToString(myReader["IGID"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }

        public List<ProjectModel> getProjectById(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.getProjectById(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.userId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectId"]);
                        tempProject.projectName = Convert.ToString(myReader["projectName"]);
                        tempProject.projectDetails = Convert.ToString(myReader["projectDetails"]);
                        tempProject.projectData = Convert.ToString(myReader["projectData"]);
                        tempProject.projectStatus = Convert.ToString(myReader["projectStatus"]);
                        tempProject.userId = Convert.ToString(myReader["useId"]);
                        tempProject.HODID = Convert.ToString(myReader["projectHOD"]);
                        tempProject.PICID = Convert.ToString(myReader["projectIncharge"]);
                        tempProject.IGID = Convert.ToString(myReader["projectInternalGuide"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }

        public List<ProjectModel> getProjectByHODId(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.getProjectByHODId(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.HODID);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectId"]);
                        tempProject.projectName = Convert.ToString(myReader["projectName"]);
                        tempProject.projectDetails = Convert.ToString(myReader["projectDetails"]);
                        tempProject.projectData = Convert.ToString(myReader["projectData"]);
                        tempProject.projectStatus = Convert.ToString(myReader["projectStatus"]);
                        tempProject.userId = Convert.ToString(myReader["Email"]);
                        tempProject.PICID = Convert.ToString(myReader["projectIncharge"]);
                        tempProject.IGID = Convert.ToString(myReader["projectInternalGuide"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }

        public List<ProjectModel> getProjectByPICId(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.getProjectByPICId(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.PICID);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectId"]);
                        tempProject.projectName = Convert.ToString(myReader["projectName"]);
                        tempProject.projectDetails = Convert.ToString(myReader["projectDetails"]);
                        tempProject.projectData = Convert.ToString(myReader["projectData"]);
                        tempProject.projectStatus = Convert.ToString(myReader["projectStatus"]);
                        tempProject.userId = Convert.ToString(myReader["useId"]);
                        tempProject.HODID = Convert.ToString(myReader["projectHOD"]);
                        tempProject.IGID = Convert.ToString(myReader["projectInternalGuide"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }
        public List<ProjectModel> getProjectByIGId(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.getProjectByIGId(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.IGID);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectId"]);
                        tempProject.projectName = Convert.ToString(myReader["projectName"]);
                        tempProject.projectDetails = Convert.ToString(myReader["projectDetails"]);
                        tempProject.projectData = Convert.ToString(myReader["projectData"]);
                        tempProject.projectStatus = Convert.ToString(myReader["projectStatus"]);
                        tempProject.userId = Convert.ToString(myReader["useId"]);
                        tempProject.PICID = Convert.ToString(myReader["projectIncharge"]);
                        tempProject.IGID = Convert.ToString(myReader["projectInternalGuide"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }

        public ProjectModel updateProject(ProjectModel project)
        {
            string query = "call projectmanagementsystem.updateProject(?,?,?,?,?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@nm", project.projectName);
                    myCommand.Parameters.AddWithValue("@pd", project.projectDetails);
                    myCommand.Parameters.AddWithValue("@pid", project.projectId);
                    myCommand.Parameters.AddWithValue("@hod", project.HODID);
                    myCommand.Parameters.AddWithValue("@pic", project.PICID);
                    myCommand.Parameters.AddWithValue("@pig", project.IGID);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return project;
            }
        }
        public ProjectModel updateStatus(ProjectModel project)
        {
            string query = "call projectmanagementsystem.updateStatus(?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {

                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@staus", project.projectStatus);
                    myCommand.Parameters.AddWithValue("@id", project.projectId);
                    if (project.projectStatus == "Approved")
                    {
                        var projectName = project.projectName;
                        var emailId = project.userId;
                        MimeMessage message = new MimeMessage();
                        MailboxAddress from = new MailboxAddress("Vinayak Bilagi", "vinayakbilagi7@gmail.com");
                        MailboxAddress to = new MailboxAddress(emailId, emailId);
                        message.From.Add(from);
                        message.To.Add(to);
                        message.Subject = "Project Approval Status";
                        BodyBuilder bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = "Congrats! Your Project "+project.projectName+" Was Approved!";
                        message.Body = bodyBuilder.ToMessageBody();
                        SmtpClient client = new SmtpClient();
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("vinayakbilagi7@gmail.com", "dddtiaivtybwqyfj");
                        client.Send(message);
                        client.Disconnect(true);
                        client.Dispose();
                    }
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return project;
            }
        }

        public List<ProjectModel> ProjectById(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.ProjectById(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.projectId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectId"]);
                        tempProject.projectName = Convert.ToString(myReader["projectName"]);
                        tempProject.projectDetails = Convert.ToString(myReader["projectDetails"]);
                        tempProject.projectData = Convert.ToString(myReader["projectData"]);
                        tempProject.projectStatus = Convert.ToString(myReader["projectStatus"]);
                        tempProject.userId = Convert.ToString(myReader["useId"]);
                        tempProject.HODID = Convert.ToString(myReader["projectHOD"]);
                        tempProject.PICID = Convert.ToString(myReader["projectIncharge"]);
                        tempProject.IGID = Convert.ToString(myReader["projectInternalGuide"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }
        public String deleteProject(int id)
        {
            string query = "call projectmanagementsystem.deleteProject(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
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

        public List<ProjectModel> studentDashboard(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.studDashboard(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.userId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectCount"]);
                        tempProject.projectName = Convert.ToString(myReader["projectStatus"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }
        public List<ProjectModel> igDashboard(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.igDashboard(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.IGID);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectCount"]);
                        tempProject.projectName = Convert.ToString(myReader["projectStatus"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }
        public List<ProjectModel> hodDashboard(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.hodDashboard(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.HODID);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectCount"]);
                        tempProject.projectName = Convert.ToString(myReader["projectStatus"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }
        public List<ProjectModel> picDashboard(ProjectModel project)
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            string query = "call projectmanagementsystem.picDashboard(?);";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", project.PICID);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        ProjectModel tempProject = new ProjectModel();
                        tempProject.projectId = Convert.ToInt32(myReader["projectCount"]);
                        tempProject.projectName = Convert.ToString(myReader["projectStatus"]);
                        projects.Add(tempProject);
                    }
                    mycon.Close();
                    return projects;
                }
            }
        }
    }
}
