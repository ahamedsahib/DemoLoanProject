using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
          
        }
        [HttpPost]
        [Route("Register")]

        public IActionResult Register([FromBody] RegisterModel userData)
        {
           
            try
            {
                
                ////sending data to manager
                string resMessage = this.manager.Register(userData);
                if (resMessage.Equals("Registration Successful"))
                {
             
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resMessage });
                }
                else
                {
                  
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resMessage });
                }
            }
            catch (Exception ex)
            {
               
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult Login([FromBody] LoginModel loginData)
        {
            try
            {
           
                var result = this.manager.Login(loginData);
                if (result!=null)
                {
                   
                    //string tokenString = this.manager.GenerateToken(loginData.EmailId);
                    return this.Ok(new { Status = true, Message = "Login Successful!!!", Data = result});
                }
                else
                {
                  
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Login UnSuccessful!!!" });
                }
            }
            catch (Exception ex)
            { 
               return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
