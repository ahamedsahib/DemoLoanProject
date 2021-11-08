// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPropertyRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Repository.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;

    /// <summary>
    /// interface for property repository class
    /// </summary>
    public interface IPropertyRepository
    {
        /// <summary>
        /// Add form definition method
        /// </summary>
        /// <param name="formList">passing a form list </param>
        /// <returns>returns a string message</returns>
        string AddForm(FormList formList);

        /// <summary>
        /// get property details definition method
        /// </summary>
        /// <param name="userId">passing a user id as integer</param>
        /// <returns>returns a list of form model</returns>
        List<FormModel> GetPropertyDetails(int userId);
    }
}
