using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IPermissionClass
    {
        public PermissionModel updatePermission(PermissionModel permission);
        public PermissionModel addPermission(PermissionModel permission);
        public string deletePermission(int id);
        public List<PermissionModel> getAll();
        public List<PermissionModel> getPermissionById(PermissionModel permission);
    }
}
