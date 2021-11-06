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
        /// Get property details
        /// </summary>
        /// <param name="userId">passing a user id as integer</param>
        /// <returns>Returns a list of proeprty details</returns>
        List<PropertyModel> GetPropertyDetails(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formList"></param>
        /// <returns></returns>
        FormModel AddForm(FormList formList);
    }
}
