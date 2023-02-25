using MySql.Data.MySqlClient;
using ProjectManagementSystem.Models;
using System.Security;

namespace ProjectManagementSystem.Business
{
    public class PermissionClass : IPermissionClass
    {
        private readonly IConfiguration _configuration;
        public PermissionClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public PermissionModel addPermission(PermissionModel permission)
        {
            string query = "projectmanagementsystem.addPermission(?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@permission", permission.permission);
                    myCommand.Parameters.AddWithValue("@role", permission.roleId);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return permission;
            }
        }
        public PermissionModel updatePermission(PermissionModel permission)
        {
            string query = "projectmanagementsystem.updatePermission(?,?,?)";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", permission.permissionId);
                    myCommand.Parameters.AddWithValue("@permission", permission.permission);
                    myCommand.Parameters.AddWithValue("@role", permission.roleId);
                    myCommand.ExecuteReader();
                    mycon.Close();
                }
                return permission;
            }
        }
        public String deletePermission(int id)
        {
            string query = "call projectmanagementsystem.deletePermission(?);";
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
        public List<PermissionModel> getAll()
        {
            List<PermissionModel> permission = new List<PermissionModel>();
            string query = "select * from permissiontable";
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
                        PermissionModel temppermission = new PermissionModel();
                        temppermission.permissionId = Convert.ToInt32(myReader["permissionId"]);
                        temppermission.permission = Convert.ToString(myReader["permission"]);
                        temppermission.roleId = Convert.ToString(myReader["roleId"]);
                        permission.Add(temppermission);
                    }
                    mycon.Close();
                    return permission;
                }
            }
        }
        public List<PermissionModel> getPermissionById(PermissionModel permission)
        {
            List<PermissionModel> permissions = new List<PermissionModel>();
            string query = "select * from permissiontable where permissionId = ?";
            string sqlDataSource = _configuration.GetConnectionString("dbConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", permission.permissionId);
                    myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        PermissionModel temppermission = new PermissionModel();
                        temppermission.permissionId = Convert.ToInt32(myReader["permissionId"]);
                        temppermission.permission = Convert.ToString(myReader["permission"]);
                        temppermission.roleId = Convert.ToString(myReader["roleId"]);
                        permissions.Add(temppermission);
                    }
                    mycon.Close();
                    return permissions;
                }
            }
        }
    }
}
