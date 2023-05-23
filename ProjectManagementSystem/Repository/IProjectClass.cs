using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Business
{
    public interface IProjectClass
    {
        ProjectModel addProject(ProjectModel projectModel);
        ProjectModel updateProject(ProjectModel projectModel);
        ProjectModel updateStatus(ProjectModel projectModel);
        public List<ProjectModel> studentDashboard(ProjectModel project);
        public List<ProjectModel> igDashboard(ProjectModel project);
        public List<ProjectModel> picDashboard(ProjectModel project);
        public List<ProjectModel> hodDashboard(ProjectModel project);
        public List<ProjectModel> getProject();
        public List<ProjectModel> getProjectById(ProjectModel project);
        public List<ProjectModel> getProjectByHODId(ProjectModel project);
        public List<ProjectModel> getProjectByPICId(ProjectModel project);
        public List<ProjectModel> getProjectByIGId(ProjectModel project);
        public List<ProjectModel> ProjectById(ProjectModel project);
        public String deleteProject(int id);
    }
}
