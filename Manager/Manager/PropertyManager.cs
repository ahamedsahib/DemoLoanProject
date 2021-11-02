using Manager.Interface;
using Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Manager
{
    public class PropertyManager: IPropertyManager
    {
        private readonly IPropertyRepository propertyRepository;


        public PropertyManager(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

   
        public PropertyModel AddProperty(PropertyModel propertyData)
        {
            try
            {
                return this.propertyRepository.AddProperty(propertyData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PropertyModel> GetPropertyDetails(int userId)
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
