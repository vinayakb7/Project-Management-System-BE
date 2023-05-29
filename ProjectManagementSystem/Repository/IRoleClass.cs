using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IRoleClass
    {
        public RoleModel updateRole(RoleModel role);
        public RoleModel addRole(RoleModel role);
        public string deleteRole(int id);
        public List<RoleModel> getAll();
        public List<RoleModel> getRoleById(RoleModel role);
    }
}
