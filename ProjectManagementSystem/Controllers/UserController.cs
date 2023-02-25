using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserClass _userClass;
        public UserController(IUserClass userClass)
        {
            _userClass = userClass;
        }
        [HttpPost]
        public IActionResult GetUser(UserModel user)
        {
                return Ok(_userClass.getUser(user));
        }

        [HttpPost]
        [Route("addUser")]
        public IActionResult addUser(UserModel user)
        {
            try
            {
                return Ok(_userClass.addUser(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("forgotPassword")]
        public IActionResult forgotPassword(Forgot forgot)
        {
            try
            {
                return Ok(_userClass.gotPassword(forgot));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("checkOTP")]
        public IActionResult checkOTP(Forgot forgot)
        {
            try
            {
                return Ok(_userClass.checkOTP(forgot));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("getEmail")]
        public IActionResult getEmail(UserModel user)
        {
            try
            {
                return Ok(_userClass.getEmail(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult getAll()
        {
            try
            {
                return Ok(_userClass.getAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("updatePassword")]
        public IActionResult updatePassword(UserModel user)
        {
            return Ok(_userClass.updatePassword(user));
        }
        [HttpPost]
        [Route("getUserById")]
        public IActionResult getUserById(UserModel user)
        {
            try
            {
                return Ok(_userClass.getUserById(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("updateUser")]
        public IActionResult updateUser(UserModel user)
        {
            try
            {
                return Ok(_userClass.updateUser(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("adminDashboard")]
        public IActionResult adminDashboard()
        {
            try
            {
                return Ok(_userClass.adminDashboard());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("updatePasswordByEmail")]
        public IActionResult updatePasswordByEmail(UserModel user)
        {
            return Ok(_userClass.updatePasswordByEmail(user));
        }

        [HttpPost]
        [Route("sendNotification")]
        public IActionResult notification(UserModel user)
        {
            try
            {
                return Ok(_userClass.notification(user));
            }
            catch(Exception e)
            {
                return BadRequest("Email Not Sent Due To "+e.Message);
            }
        }

        [HttpDelete]
        [Route("deleteUser")]
        public IActionResult deleteUser(int id)
        {
            return Ok(_userClass.deleteUser(id));
        }

    }
}
