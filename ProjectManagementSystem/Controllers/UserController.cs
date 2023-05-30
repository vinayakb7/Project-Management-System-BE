using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :  BaseController
    {
        private readonly IUserClass usersClass;
        public UserController(IUserClass userClass)
        {
            this.usersClass = userClass;
        }

        /// <summary>
        /// API for Log in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        [HttpPost("login")]
        public IActionResult GetUser(UserModel user)
        {
            try
            {
                Result<IEnumerable<UserModel>> result = new();
                result = usersClass.GetUserForLogIn(user);
                return result.IsSuccessfull ? Ok(result) : Results(result);
            }
            catch (Exception ex)
            {
                throw new CustomException(new Exception("Error occured while getting users records " + ex.Message));
            }
        }

        /// <summary>
        /// API to add user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addUser")]
        public IActionResult AddUser(UserModel user)
        {
            try
            {
                Result<int> result = new();
                result = usersClass.AddUser(user);
                return result.IsSuccessfull ? Ok(result) : Results(result);
            }
            catch (Exception ex)
            {
                throw new CustomException(new Exception("Error occured while getting users records " + ex.Message));
            }
        }
        [HttpPost]
        [Route("forgotPassword")]
        public IActionResult forgotPassword(Forgot forgot)
        {
            try
            {
                return Ok(usersClass.SendOTP(forgot));
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
                return Ok(usersClass.CheckOTP(forgot));
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
                return Ok(usersClass.GetEmail(user));
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
                return Ok(usersClass.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// API to update user password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatePassword")]
        public IActionResult updatePassword(UserModel user)
        {
            try
            {
                Result<UserModel> result = new();
                result = usersClass.UpdatePassword(user);
                return result.IsSuccessfull ? Ok(result) : Results(result);
            }
            catch (Exception ex)
            {
                throw new CustomException(new Exception("Error occured while updating user password " + ex.Message));
            }
        }
        [HttpPost]
        [Route("getUserById/{userId}")]
        public IActionResult getUserById(int userId)
        {
            try
            {
                Result<IEnumerable<UserModel>> result = new();
                result = usersClass.GetUserById(userId);
                return result.IsSuccessfull ? Ok(result) : Results(result);
            }
            catch (Exception ex)
            {
                throw new CustomException(new Exception("Error occured while getting users records " + ex.Message));
            }
        }

        [HttpPost]
        [Route("updateUser")]
        public IActionResult updateUser(UserModel user)
        {
            try
            {
                return Ok(usersClass.UpdateUser(user));
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
                return Ok(usersClass.AdminDashboard());
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
            return Ok(usersClass.UpdatePasswordByEmail(user));
        }

        [HttpPost]
        [Route("sendNotification")]
        public IActionResult notification(UserModel user)
        {
            try
            {
                return Ok(usersClass.SendNotification(user));
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
            return Ok(usersClass.DeleteUser(id));
        }

    }
}
