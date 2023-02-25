using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectClass _projectClass;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProjectController(IProjectClass projectClass, IWebHostEnvironment webHostEnvironment)
        {
            _projectClass = projectClass;

            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("secondpost")]
        public IActionResult uploadFiles(List<IFormFile> files)
        {
            if (files.Count == 0)
                return BadRequest();
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploadedProjects");
            foreach (var file in files)
            {
                var ext = System.IO.Path.GetExtension(file.FileName);
                if (ext == ".zip")
                {
                    string filepath = Path.Combine(directoryPath, file.FileName);
                    if (System.IO.File.Exists(filepath))
                        return BadRequest("Ooops!  File Already Exist!?");
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok("File Uploaded");
                }
            }
            return BadRequest("Upload Only Zip File");
        }

        [HttpPost]
        [Route("addProject")]
        public IActionResult addProject(ProjectModel project)
        {
            return Ok(_projectClass.addProject(project));
        }

        [HttpGet]
        public IActionResult getProject()
        {
            return Ok(_projectClass.getProject());
        }

        [HttpPost]
        [Route("GetById")]
        public IActionResult getProjectById(ProjectModel project)
        {
            return Ok(_projectClass.getProjectById(project));
        }
        [HttpPost]
        [Route("ById")]
        public IActionResult ProjectById(ProjectModel project)
        {
            return Ok(_projectClass.ProjectById(project));
        }
        [HttpPost]
        [Route("ByHODId")]
        public IActionResult ProjectByHODId(ProjectModel project)
        {
            return Ok(_projectClass.getProjectByHODId(project));
        }
        [HttpPost]
        [Route("ByPICId")]
        public IActionResult ProjectByPICId(ProjectModel project)
        {
            return Ok(_projectClass.getProjectByPICId(project));
        }
        [HttpPost]
        [Route("ByIGId")]
        public IActionResult ProjectByIGId(ProjectModel project)
        {
            return Ok(_projectClass.getProjectByIGId(project));
        }
        [HttpPost]
        [Route("projectDetails")]
        public IActionResult studentDashboard(ProjectModel project)
        {
            return Ok(_projectClass.studentDashboard(project));
        }
        [HttpPost]
        [Route("HODProjectDetails")]
        public IActionResult hodDashboard(ProjectModel project)
        {
            return Ok(_projectClass.hodDashboard(project));
        }
        [HttpPost]
        [Route("IgProjectDetails")]
        public IActionResult igDashboard(ProjectModel project)
        {
            return Ok(_projectClass.igDashboard(project));
        }
        [HttpPost]
        [Route("PicProjectDetails")]
        public IActionResult picDashboard(ProjectModel project)
        {
            return Ok(_projectClass.picDashboard(project));
        }
        [HttpDelete]
        public string deleteProject(int id)
        {
            return(_projectClass.deleteProject(id));
        }
        [HttpPut]
        public IActionResult updateProject(ProjectModel project)
        {
            return Ok(_projectClass.updateProject(project));
        }
        [HttpPut]
        [Route("updateStatus")]
        public IActionResult updateStatus(ProjectModel project)
        {
            return Ok(_projectClass.updateStatus(project));
        }
        [HttpGet]
        [Route("Download File")]
        public async Task<IActionResult> DownloadFile(string nameFile)
        {
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "uploadedProjects", nameFile);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(directoryPath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(directoryPath);
            return File(bytes, contentType, Path.GetFileName(directoryPath));
        }
    }
}
