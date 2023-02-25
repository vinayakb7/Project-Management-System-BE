using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionClass _permissionClass;
        public PermissionController(IPermissionClass permissionClass)
        {
            _permissionClass = permissionClass;
        }

        [HttpPost]
        [Route("addPermission")]
        public IActionResult addPermission(PermissionModel permission)
        {
            try
            {
                return Ok(_permissionClass.addPermission(permission));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("updatePermission")]
        public IActionResult updateRole(PermissionModel permission)
        {
            try
            {
                return Ok(_permissionClass.updatePermission(permission));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("getPermissionById")]
        public IActionResult getPermissionById(PermissionModel permission)
        {
            try
            {
                return Ok(_permissionClass.getPermissionById(permission));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("deletePermission")]
        public IActionResult deleteRole(int id)
        {
            try
            {
                return Ok(_permissionClass.deletePermission(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getPermission")]
        public IActionResult getAll()
        {
            try
            {
                return Ok(_permissionClass.getAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
