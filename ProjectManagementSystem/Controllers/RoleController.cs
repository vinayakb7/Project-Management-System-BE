using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleClass _roleClass;
        public RoleController(IRoleClass roleClass)
        {
            _roleClass = roleClass;
        }

        [HttpPost]
        [Route("addRole")]
        public IActionResult addRole(RoleModel role)
        {
            try
            {
                return Ok(_roleClass.addRole(role));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("updateRole")]
        public IActionResult updateRole(RoleModel role)
        {
            try
            {
                return Ok(_roleClass.updateRole(role));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("getRoleById")]
        public IActionResult getRoleById(RoleModel role)
        {
            try
            {
                return Ok(_roleClass.getRoleById(role));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("deleteRole")]
        public IActionResult deleteRole(int id)
        {
            try
            {
                return Ok(_roleClass.deleteRole(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getRoles")]
        public IActionResult getAll()
        {
            try
            {
                return Ok(_roleClass.getAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
