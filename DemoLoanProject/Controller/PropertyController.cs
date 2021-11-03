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
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyManager propertyManager;

        public PropertyController(IPropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }

        [HttpPost]
        [Route("Property")]
        public IActionResult AddProperty([FromBody] List<PropertyModel> propertyData,int formId, int userId)
        {
            try
            {
                var message = this.propertyManager.AddProperty(propertyData,formId,userId);
                if (message != null)
                {
                    return this.Ok(new { Status = true,  Message = "Added Sucessfully" ,Data=message});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not added successfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("AddForm")]
        public IActionResult AddForm([FromBody] FormList formList)
        {
            try
            {
                var message = this.propertyManager.AddForm(formList);
                if (message != null)
                {
                    return this.Ok(new { Status = true, Message = "Added Sucessfully", Data = message });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Not added successfully" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("Property")]
        public IActionResult GetPropertyDetails(int userId)
        {
            try
            {
                var resMessage = this.propertyManager.GetPropertyDetails(userId);
                if (resMessage != null)
                {
                    return this.Ok(new { Status = true, Message = "Property Details returned successfully", Data = resMessage });
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
