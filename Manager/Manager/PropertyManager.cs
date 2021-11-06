// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyManager.cs" company="TVSNEXT">
//   Copyright © 2021 Company="TVSNEXT"
// </copyright>
// <creator name="Radhika"/>
// ----------------------------------------------------------------------------------------------------------

namespace Manager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using global::Manager.Interface;
    using Models;
    using Repository.Interface;

    /// <summary>
    /// Class Property manager
    /// </summary>
    public class PropertyManager : IPropertyManager
    {
        /// <summary>
        /// declaring repository
        /// </summary>
        private readonly IPropertyRepository propertyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyManager"/> class
        /// </summary>
        /// <param name="propertyRepository">property repository as parameter</param>
        public PropertyManager(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        /// <summary>
        /// Add Form 
        /// </summary>
        /// <param name="formList">passing a form list </param>
        /// <returns>Returns a string message</returns>
        public string AddForm(FormList formList)
        {
            try
            {
                return this.propertyRepository.AddForm(formList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get Property Details 
        /// </summary>
        /// <param name="userId">passing a user id as integer</param>
        /// <returns>returns a list of property details</returns>
        public List<FormModel> GetPropertyDetails(int userId)
        {
            try
            {
                return this.propertyRepository.GetPropertyDetails(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
