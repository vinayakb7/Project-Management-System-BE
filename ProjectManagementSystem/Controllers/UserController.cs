using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Mozilla;
using ProjectManagementSystem.Business;
using ProjectManagementSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManagementSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :  BaseController
    {
        private readonly IUserClass usersClass;
        private readonly IConfiguration configuration;
        public UserController(IUserClass userClass, IConfiguration configuration)
        {
            this.usersClass = userClass;
            this.configuration = configuration;
        }

        /// <summary>
        /// API for Log in.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult GetUser(User user)
        {
            try
            {
                IActionResult response = Unauthorized();
                var user_ = AuthenticateUser(user);
                if (user_ != null)
                {
                    var token = GenerateToken(user_);
                    response = Ok(new { user_ ,token = token });
                }
                return response;
                //Result<IEnumerable<UserModel>> result = new();
                //result = usersClass.GetUserForLogIn(user);
                //return result.IsSuccessfull ? Ok(result) : Results(result);
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
        public IActionResult AddUser(User user)
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
        public IActionResult getEmail(User user)
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
        public IActionResult updatePassword(User user)
        {
            try
            {
                Result<User> result = new();
                result = usersClass.UpdatePassword(user);
                return result.IsSuccessfull ? Ok(result) : Results(result);
            }
            catch (Exception ex)
            {
                throw new CustomException(new Exception("Error occured while updating user password " + ex.Message));
            }
        }

        [Authorize]
        [HttpPost]
        [Route("getUserById/{userId}")]
        public IActionResult getUserById(int userId)
        {
            try
            {
                Result<IEnumerable<User>> result = new();
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
        public IActionResult updateUser(User user)
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
        public IActionResult updatePasswordByEmail(User user)
        {
            return Ok(usersClass.UpdatePasswordByEmail(user));
        }

        [HttpPost]
        [Route("sendNotification")]
        public IActionResult notification(User user)
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

        private User AuthenticateUser(User user)
        {
            User _user = usersClass.GetUserForLogIn(user).Data.FirstOrDefault();
            if (_user != null)
            {
                return _user;
            }
            return _user;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.userEmail),
                new Claim(ClaimTypes.Role,user.userRole)
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
