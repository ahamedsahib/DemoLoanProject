// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPropertyManager.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;

    /// <summary>
    /// interface for user manager class
    /// </summary>
    public interface IPropertyManager
    {
        /// <summary>
        /// Add Form 
        /// </summary>
        /// <param name="formList">passing a form list </param>
        /// <returns>returns a string message</returns>
        string AddForm(FormList formList);

        /// <summary>
        /// Get property details
        /// </summary>
        /// <param name="userId">passing a user id as integer</param>
        /// <returns>Returns a list of property details</returns>
        List<FormModel> GetPropertyDetails(int userId);
    }
}
