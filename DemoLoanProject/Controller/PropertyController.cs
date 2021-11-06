// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyController.cs" company="TVSNEXT">
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
    public class PropertyController : ControllerBase
    {
        /// <summary>
        /// instance property manager 
        /// </summary>
        private readonly IPropertyManager propertyManager;

        /// <summary>
        /// instance for logger
        /// </summary>
        private readonly ILogger<PropertyController> logger;

        /// <summary>
        ///  Initializes a new instance of the <see cref="PropertyController"/> class
        /// </summary>
        /// <param name="propertyManager">passing a manager parameter</param>
        /// <param name="logger">passing a logger parameter</param>
        public PropertyController(IPropertyManager propertyManager, ILogger<PropertyController> logger)
        {
            this.propertyManager = propertyManager;
            this.logger = logger;
        }

        /// <summary>
        /// Add Form Data
        /// </summary>
        /// <param name="formList">passing a formList model</param>
        /// <returns>Returns a formList data and message </returns>
        [HttpPost]
        [Route("AddForm")]
        public IActionResult AddForm([FromBody] FormList formList)
        {
            try
            {
                var message = this.propertyManager.AddForm(formList);
                if (message != null)
                {
                    return this.Ok(new { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogWarning("Form Data Not added successfully");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception Occured While Add data in Form " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Get property details
        /// </summary>
        /// <param name="userId">passing a userId</param>
        /// <returns>returns a property details and message</returns>
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
                    this.logger.LogWarning("UserId does not exist");
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "UserId does not exist" });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Exception Occured While Get the property details " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
    }
}
