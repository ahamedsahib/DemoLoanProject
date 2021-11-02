using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoLoanProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager usermanager;

        private readonly ILogger<UserController> logger;
        public UserController(IUserManager usermanager, ILogger<UserController> logger)
        {
            this.usermanager = usermanager;
            this.logger = logger;

        }
        [HttpPost]
        [Route("Register")]

        public IActionResult Register([FromBody] RegisterModel userData)
        {
            try
            {
                string resMessage = this.usermanager.Register(userData);
                if (resMessage.Equals("Registration Successful"))
                {
             
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resMessage });
                }
                else
                {
                    this.logger.LogWarning("Registration Unsuccesfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resMessage });
                }
            }
            catch (Exception ex)
            {

                this.logger.LogError("Exception Occured While Register " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult Login([FromBody] LoginModel loginData)
        {
            try
            {
                var result = this.usermanager.Login(loginData);
                if (result!=null)
                {
                   
                    //string tokenString = this.manager.GenerateToken(loginData.EmailId);
                    return this.Ok(new { Status = true, Message = "Login Successful!!!", Data = result});
                }
                else
                {
                    this.logger.LogWarning("Login Unsuccessfull");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login UnSuccessful!!!" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception Occured While log in " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


        [HttpGet]
        [Route("Users")]
        public IActionResult GetUserDetails(int userId)
        {
            try
            {
                var resMessage = this.usermanager.GetUserDetails(userId);
                if (resMessage != null)
                {
                    return this.Ok(new { Status = true, Message = "UserDetails returned successfully", Data = resMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "UserId does not exist" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


    }
}
