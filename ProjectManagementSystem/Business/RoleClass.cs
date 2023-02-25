using MySql.Data.MySqlClient;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public class RoleClass : IRoleClass
    {
        private readonly IConfiguration _configuration;
        public RoleClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public RoleModel addRole(RoleModel role)
        {
            string query = "projectmanagementsystem.addRole(?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@role", role.role);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return role;
            }
        }
        public RoleModel updateRole(RoleModel role)
        {
            string query = "projectmanagementsystem.updateRole(?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", role.roleId);
                    myCommand.Parameters.AddWithValue("@pass", role.role);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return role;
            }
        }
        public String deleteRole(int id)
        {
            string query = "call projectmanagementsystem.deleteRole(?);";
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
        public List<RoleModel> getAll()
        {
            List<RoleModel> role = new List<RoleModel>();
            string query = "select * from role";
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
                        RoleModel temprole = new RoleModel();
                        temprole.roleId = Convert.ToInt32(myReader["roleId"]);
                        temprole.role = Convert.ToString(myReader["role"]);
                        role.Add(temprole);
                    }
                    mycon.Close();
                    return role;
                }
            }
        }
        public List<RoleModel> getRoleById(RoleModel role)
        {
            List<RoleModel> roles = new List<RoleModel>();
            string query = "select * from role where roleId = ?";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", role.roleId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        RoleModel tmeprole = new RoleModel();
                        tmeprole.roleId = Convert.ToInt32(myReader["roleId"]);
                        tmeprole.role = Convert.ToString(myReader["role"]);
                        roles.Add(tmeprole);
                    }
                    mycon.Close();
                    return roles;
                }
            }
        }
    }
}
