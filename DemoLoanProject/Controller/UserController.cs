// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace DemoLoanProject.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Manager.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
 
    /// <summary>
    /// Controller class-controlling API
    /// </summary>
    /// [Route("api/[controller]")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// instance user manager 
        /// </summary>
        private readonly IUserManager usermanager;

        /// <summary>
        /// instance for logger
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        ///  Initializes a new instance of the <see cref="UserController"/> class
        /// </summary>
        /// <param name="usermanager">passing a manager parameter</param>
        /// <param name="logger">passing a logger parameter</param>
        public UserController(IUserManager usermanager, ILogger<UserController> logger)
        {
            this.usermanager = usermanager;
            this.logger = logger;
        }

        /// <summary>
        /// controller-register  method
        /// </summary>
        /// <param name="userData">passing a register model data</param>
        /// <returns>return http status if registered successfully</returns>
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

        /// <summary>
        /// login API for already existing user 
        /// </summary>
        /// <param name="loginData">login model data</param>
        /// <returns>returns http status if logged in successfully</returns>
        [HttpPost]
        [Route("Login")]

       public IActionResult Login([FromBody] LoginModel loginData)
        {
            try
            {
                var result = this.usermanager.Login(loginData);
                if (result != null)
                { 
                    return this.Ok(new { Status = true, Message = "Login Successful!!!", Data = result });
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
    }
}
