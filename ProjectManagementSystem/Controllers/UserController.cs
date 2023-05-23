﻿using Microsoft.AspNetCore.Http;
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
        private readonly IUserClass usersClass;
        public UserController(IUserClass userClass)
        {
            this.usersClass = userClass;
        }
        [HttpPost]
        public IActionResult GetUser(UserModel user)
        {
                return Ok(usersClass.getUser(user));
        }

        [HttpPost]
        [Route("addUser")]
        public IActionResult addUser(UserModel user)
        {
            try
            {
                return Ok(usersClass.addUser(user));
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
                return Ok(usersClass.gotPassword(forgot));
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
                return Ok(usersClass.checkOTP(forgot));
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
                return Ok(usersClass.getEmail(user));
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
                return Ok(usersClass.getAll());
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
            return Ok(usersClass.updatePassword(user));
        }
        [HttpPost]
        [Route("getUserById")]
        public IActionResult getUserById(UserModel user)
        {
            try
            {
                return Ok(usersClass.getUserById(user));
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
                return Ok(usersClass.updateUser(user));
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
                return Ok(usersClass.adminDashboard());
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
            return Ok(usersClass.updatePasswordByEmail(user));
        }

        [HttpPost]
        [Route("sendNotification")]
        public IActionResult notification(UserModel user)
        {
            try
            {
                return Ok(usersClass.notification(user));
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
            return Ok(usersClass.deleteUser(id));
        }

    }
}
